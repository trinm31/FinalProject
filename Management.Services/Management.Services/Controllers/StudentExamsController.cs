using System.Text;
using AutoMapper;
using ExcelDataReader;
using Management.Services.BackgroudService;
using Management.Services.DbContext;
using Management.Services.Dtos;
using Management.Services.Messages;
using Management.Services.Models;
using Management.Services.RabbitMQSender;
using Management.Services.Services.IRepository;
using Management.Services.Utiliy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Management.Services.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = SD.Admin + "," + SD.Staff)]
public class StudentExamsController : Controller
{
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger _logger;

    private static string _fileName;
    private readonly IMapper _mapper;
    private readonly IRabbitMQManagementMessageSender _rabbitMqManagementMessageSender;
    private readonly BackgroundWorkerQueue _backgroundWorkerQueue;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly ApplicationDbContext _db;

    public StudentExamsController(
        IWebHostEnvironment webHostEnvironment,
        IUnitOfWork unitOfWork, 
        ILogger<ExamsController> logger,
        IMapper mapper,
        IRabbitMQManagementMessageSender rabbitMqManagementMessageSender,
        BackgroundWorkerQueue backgroundWorkerQueue, 
        IServiceScopeFactory serviceScopeFactory,
        ApplicationDbContext db
    )
    {
        _webHostEnvironment = webHostEnvironment;
        _unitOfWork = unitOfWork;
        _logger = logger;
        _mapper = mapper;
        _rabbitMqManagementMessageSender = rabbitMqManagementMessageSender;
        _backgroundWorkerQueue = backgroundWorkerQueue;
        _serviceScopeFactory = serviceScopeFactory;
        _db = db;
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> StudentExamPagination([FromQuery] PaginationDto paginationDto)
    {
        var values = _db.StudentExams
            .OrderBy(x=>x.StudentId)
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

            _backgroundWorkerQueue.QueueBackgroundWorkItem(async token =>
            {
                await GetStudent(file.FileName);
            });
            
            
            return Ok();
        }
        catch (Exception exception)
        {
            _logger.LogInformation(exception.Message);
            return StatusCode(500, $"Internal server error: {exception.Message}");
        }
    }
    
    private async Task GetStudent(string fName)
    {
         var errorCount = 0;
         var examCount = 0;
         var studentExam = new List<StudentExam>();
         using (var scope = _serviceScopeFactory.CreateScope())
         {
             var db = scope.ServiceProvider.GetService<ApplicationDbContext>();
                
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
                     var studenttemp = new StudentExam()
                     {
                         ExamId = reader.GetValue(0).ToString(),
                         StudentId = reader.GetValue(1).ToString(),
                     };
                     
                     var studentCheck = await db.Students.AnyAsync(e => e.StudentId.Contains(studenttemp.StudentId));
                     var examCheck = await db.Exams.AnyAsync(e => e.ExamId.Contains(studenttemp.ExamId));
                     
                     if (studentCheck && examCheck)
                     {
                         studentExam.Add(studenttemp);
                     }
                 }

                 List<StudentCourse> studentcourseList = new List<StudentCourse>();

                 foreach (var se in studentExam)
                 {
                     studentcourseList.Add(new StudentCourse()
                     {
                         CourseId = se.ExamId,
                         StudentId = se.StudentId
                     });
                 }
                
                 SchedulingCreateStudentCourseMessage schedulingCreateStudentCourseMessage = new SchedulingCreateStudentCourseMessage()
                 {
                    StudentCourses = studentcourseList
                 };
                 
                 try
                 {
                     _rabbitMqManagementMessageSender.SendMessage(schedulingCreateStudentCourseMessage, "schedulingcreatestudentcoursemessagequeue");
                 }
                 catch (Exception e)
                 {
                     throw;
                 }
                 
                 await db.StudentExams.AddRangeAsync(studentExam);
             }
             catch (Exception exception)
             {
                 
             }
             
             db.SaveChanges();
         }
         
         FileInfo fileNeedToDeleted = new FileInfo(fName);
            
         if (fileNeedToDeleted.Exists)
         {  
             fileNeedToDeleted.Delete();
         }  
        
    }
    
    [HttpGet("[action]")]
    public async Task<IActionResult> GetAllStudentExam()
    {
        var studentExams = await _unitOfWork.StudentExam.GetAllAsync();
        return Ok(_mapper.Map<List<StudentExamDto>>(studentExams));
    }

    //Post :: Create
    [HttpPost("[action]")]
    public async Task<IActionResult> Create([FromBody] StudentExamDto studentExamDto)
    {
        var studentExam = _mapper.Map<StudentExam>(studentExamDto);
        if (ModelState.IsValid)
        {
            if (!await _unitOfWork.StudentExam.CheckExistStudentExam(studentExam))
            {
                return BadRequest(ModelState);
            }
            await _unitOfWork.StudentExam.AddAsync(studentExam);
            _unitOfWork.Save();
            
            SchedulingCrudStudentCourseMessage schedulingCrudStudentCourseMessage = new SchedulingCrudStudentCourseMessage()
            {
                MethodType = "create",
                CourseId = studentExam.ExamId,
                StudentId = studentExam.StudentId
            };
                     
            try
            {
                _rabbitMqManagementMessageSender.SendMessage(schedulingCrudStudentCourseMessage, "schedulingcrudstudentcoursemessagequeue");
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
    public async Task<IActionResult> GetStudentExamById(int id)
    {
        var studentExam = await _unitOfWork.StudentExam.GetAsync(id);
        if (studentExam == null)
        {
            return NotFound();
        }
        return Ok(_mapper.Map<StudentExamDto>(studentExam));
    }
    
   
    [HttpPost("[action]")]
    public async Task<IActionResult> Edit([FromBody] StudentExamDto studentExamDto)
    {
        var studentExam = _mapper.Map<StudentExam>(studentExamDto);
        if (ModelState.IsValid)
        {

            var oldStudentExam = await _unitOfWork.StudentExam.GetAsync(studentExamDto.Id);
            if (!await _unitOfWork.StudentExam.CheckExistStudentExam(studentExam))
            {
                return BadRequest(ModelState);
            }
            
            SchedulingCrudStudentCourseMessage schedulingCrudStudentCourseMessage = new SchedulingCrudStudentCourseMessage()
            {
                MethodType = "update",
                CourseId = studentExam.ExamId,
                StudentId = studentExam.StudentId,
                oldCourseId = oldStudentExam.ExamId,
                oldStudentId = oldStudentExam.StudentId
            };
                     
            try
            {
                _rabbitMqManagementMessageSender.SendMessage(schedulingCrudStudentCourseMessage, "schedulingcrudstudentcoursemessagequeue");
            }
            catch (Exception e)
            {
                throw;
            }
            
            await _unitOfWork.StudentExam.Update(studentExam);
            _unitOfWork.Save();
            return Ok();
        }
        return BadRequest(ModelState);
    }
    
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var studentExam = await _unitOfWork.StudentExam.GetAsync(id);
        SchedulingCrudStudentCourseMessage schedulingCrudStudentCourseMessage = new SchedulingCrudStudentCourseMessage()
        {
            MethodType = "delete",
            CourseId = studentExam.ExamId,
            StudentId = studentExam.StudentId
        };
                     
        try
        {
            _rabbitMqManagementMessageSender.SendMessage(schedulingCrudStudentCourseMessage, "schedulingcrudstudentcoursemessagequeue");
        }
        catch (Exception e)
        {
            throw;
        }
        
        await _unitOfWork.StudentExam.RemoveAsync(id);
        _unitOfWork.Save();
        return Ok();
    }
    
    
}