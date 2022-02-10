using System.Text;
using AutoMapper;
using ExcelDataReader;
using Management.Services.Dtos;
using Management.Services.Models;
using Management.Services.Services.IRepository;
using Management.Services.Utiliy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

    public StudentExamsController(
        IWebHostEnvironment webHostEnvironment,
        IUnitOfWork unitOfWork, 
        ILogger<ExamsController> logger,
        IMapper mapper
    )
    {
        _webHostEnvironment = webHostEnvironment;
        _unitOfWork = unitOfWork;
        _logger = logger;
        _mapper = mapper;
    }
    
    [HttpPost("[action]")]
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

            var studentExam = GetStudent(file.FileName);
            
            FileInfo fileNeedToDeleted = new FileInfo(fileName);
            
            if (fileNeedToDeleted.Exists)
            {  
                fileNeedToDeleted.Delete();
            }  
            
            return Ok(_mapper.Map<List<StudentExamDto>>(studentExam.Result));
        }
        catch (Exception exception)
        {
            _logger.LogInformation(exception.Message);
            return StatusCode(500, $"Internal server error: {exception.Message}");
        }
    }
    
    private async Task<List<StudentExam>> GetStudent(string fName)
    {
         var errorCount = 0;
         var examCount = 0;
         var studentExam = new List<StudentExam>();
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
                 studentExam.Add(new StudentExam()
                 {
                     ExamId = reader.GetValue(0).ToString(),
                     StudentId = reader.GetValue(1).ToString(),
                 });
                 
             }
             await _unitOfWork.StudentExam.AddRangeAsync(studentExam);
         }
         catch (Exception exception)
         {
             
         }
         
         _unitOfWork.Save();
       
         return studentExam;
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
            if (await _unitOfWork.StudentExam.CheckExistStudentExam(studentExam))
            {
                return BadRequest(ModelState);
            }
            await _unitOfWork.StudentExam.AddAsync(studentExam);
            _unitOfWork.Save();
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
            if (await _unitOfWork.StudentExam.CheckExistStudentExam(studentExam))
            {
                return BadRequest(ModelState);
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
        await _unitOfWork.StudentExam.RemoveAsync(id);
        _unitOfWork.Save();
        return Ok();
    }
    
    
}