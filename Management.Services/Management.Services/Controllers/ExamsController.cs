using System.Text;
using AutoMapper;
using ExcelDataReader;
using Management.Services.DbContext;
using Management.Services.Dtos;
using Management.Services.Messages;
using Management.Services.Models;
using Management.Services.RabbitMQSender;
using Management.Services.Services.IRepository;
using Management.Services.Utiliy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Management.Services.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = SD.Admin + "," + SD.Staff)]
public class ExamsController : ControllerBase
{
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger _logger;

    private static string _fileName;
    private readonly IMapper _mapper;
    private readonly IRabbitMQManagementMessageSender _rabbitMqManagementMessageSender;
    private readonly ApplicationDbContext _db;

    public ExamsController(
        IWebHostEnvironment webHostEnvironment,
        IUnitOfWork unitOfWork,
        ILogger<ExamsController> logger,
        IMapper mapper,
        IRabbitMQManagementMessageSender rabbitMqManagementMessageSender,
        ApplicationDbContext db
    )
    {
        _webHostEnvironment = webHostEnvironment;
        _unitOfWork = unitOfWork;
        _logger = logger;
        _mapper = mapper;
        _rabbitMqManagementMessageSender = rabbitMqManagementMessageSender;
        _db = db;
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> StudentsPagination([FromQuery] PaginationDto paginationDto)
    {
        var values = _db.Exams
            .OrderBy(x=>x.ExamId)
            .Skip((paginationDto.PageNumber - 1) * paginationDto.PageSize)
            .Take(paginationDto.PageSize)
            .ToList();

        return Ok(values);
    }
    
    [HttpPost("[action]")]
    [RequestSizeLimit(100_000_000)]
    public async Task<IActionResult> Upload([FromForm] FileModel file)
    {
        try
        {
            if (file == null)
            {
                return BadRequest();
            }
            var separator = Path.DirectorySeparatorChar;
            string fileName = $"{_webHostEnvironment.WebRootPath}{separator}files{separator}{file.FileName}";

            _fileName = file.FileName.Split(".")[0];

            await using (FileStream fileStream = System.IO.File.Create(fileName))
            {
                await file.FormFile[0].CopyToAsync(fileStream);
                fileStream.Flush();
            }

            var exams = GetExamList(file.FileName);
            
            FileInfo fileNeedToDeleted = new FileInfo(fileName);
            
            if (fileNeedToDeleted.Exists)
            {  
                fileNeedToDeleted.Delete();
            }  
            
            return Ok(_mapper.Map<List<ExamDto>>(exams.Result));
        }
        catch (Exception exception)
        {
            _logger.LogInformation(exception.Message);
            return StatusCode(500, $"Internal server error: {exception.Message}");
        }
    }
    
    private async Task<List<Exam>> GetExamList(string fName)
    {
         var errorCount = 0;
         var examCount = 0;
         var exams = new List<Exam>();
         try
         {
             var separator = Path.DirectorySeparatorChar;
             var fileName = $"{Directory.GetCurrentDirectory()}{$"{separator}wwwroot{separator}files"}{separator}" + fName;
             Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
             await using var stream = System.IO.File.Open(fileName, FileMode.Open, FileAccess.Read);
             using var reader = ExcelReaderFactory.CreateReader(stream);
             reader.AsDataSet(new ExcelDataSetConfiguration
             {
                 ConfigureDataTable = _ => new ExcelDataTableConfiguration
                 {
                     UseHeaderRow = true
                 }
             });

             while (reader.Read())
             {
                 var doesExamExist = await _unitOfWork.Exam.GetAllAsync(
                     s => s.ExamId == reader.GetValue(0).ToString()
                 );

                 if (doesExamExist.Any())
                 {
                     errorCount++;
                 }
                 else
                 {
                     var exam = new Exam()
                     {
                         ExamId = reader.GetValue(0).ToString(),
                         Name = reader.GetValue(1).ToString(),
                         Description = reader.GetValue(2).ToString(),
                         Status = true,
                     };
                     
                     exams.Add(exam);
                     
                     SchedulingCrudCourseMessage schedulingCrudCourseMessage = new SchedulingCrudCourseMessage()
                     {
                         MethodType = "create",
                         Id = exam.ExamId,
                         Name = exam.ExamId
                     };
                     try
                     {
                         _rabbitMqManagementMessageSender.SendMessage(schedulingCrudCourseMessage, "schedulingcrudcoursemessagequeue");
                     }
                     catch (Exception e)
                     {
                         throw;
                     }
                     
                     examCount++;
                 }
             }
             await _unitOfWork.Exam.AddRangeAsync(exams);
         }
         catch (Exception exception)
         {
             
         }
         
         if (errorCount > 0)
         {
             
         }
         _unitOfWork.Save();
       
         return exams;
    }
    
    [HttpGet("[action]")]
    public async Task<IActionResult> GetAllExam()
    {
        var exams = await _unitOfWork.Exam.GetAllAsync();
        return Ok(_mapper.Map<List<ExamDto>>(exams));
    }

    //Post :: Create
    [HttpPost("[action]")]
    public async Task<IActionResult> Create([FromBody] ExamCreateDto examDto)
    {
        var exam = _mapper.Map<Exam>(examDto);
        if (ModelState.IsValid)
        {
            if (await _unitOfWork.Exam.CheckExistExam(exam))
            {
                return BadRequest(ModelState);
            }
            await _unitOfWork.Exam.AddAsync(exam);
            _unitOfWork.Save();

            SchedulingCrudCourseMessage schedulingCrudCourseMessage = new SchedulingCrudCourseMessage()
            {
                MethodType = "create",
                Id = exam.ExamId,
                Name = exam.ExamId
            };
            try
            {
                _rabbitMqManagementMessageSender.SendMessage(schedulingCrudCourseMessage, "schedulingcrudcoursemessagequeue");
            }
            catch (Exception e)
            {
                throw;
            }
           
            
            return Ok();
        }
        return BadRequest(ModelState);
    }
    
    [HttpGet("[action]/{id:int}")]
    public async Task<IActionResult> GetExamById(int id)
    {
        var exam = await _unitOfWork.Exam.GetAsync(id);
        if (exam == null)
        {
            return NotFound();
        }
        return Ok(_mapper.Map<ExamDto>(exam));
    }
    
   
    [HttpPost("[action]")]
    public async Task<IActionResult> Edit([FromBody] ExamDto examDto)
    {
        var exam = _mapper.Map<Exam>(examDto);
        var examMessage = await _unitOfWork.Exam.GetAsync(exam.Id);
        if (ModelState.IsValid)
        {
            SchedulingCrudCourseMessage schedulingCrudCourseMessage = new SchedulingCrudCourseMessage()
            {
                MethodType = "update",
                Id = examMessage.ExamId,
                Name = exam.ExamId
            };
            try
            {
                _rabbitMqManagementMessageSender.SendMessage(schedulingCrudCourseMessage, "schedulingcrudcoursemessagequeue");
            }
            catch (Exception e)
            {
                throw;
            }

            await _unitOfWork.Exam.Update(exam);
            _unitOfWork.Save();
            return Ok();
            
        }
        return BadRequest(ModelState);
    }
    
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var exam = await _unitOfWork.Exam.GetAsync(id);
        await _unitOfWork.Exam.RemoveAsync(id);
        _unitOfWork.Save();

        SchedulingCrudCourseMessage schedulingCrudCourseMessage = new SchedulingCrudCourseMessage()
        {
            MethodType = "delete",
            Id = exam.ExamId,
            Name = exam.ExamId
        };
        try
        {
            _rabbitMqManagementMessageSender.SendMessage(schedulingCrudCourseMessage, "schedulingcrudcoursemessagequeue");
        }
        catch (Exception e)
        {
            throw;
        }
        
        return Ok();
    }
}