using System.Drawing;
using System.Drawing.Imaging;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using ExcelDataReader;
using Management.Services.Dtos;
using Management.Services.Models;
using Management.Services.Services.IRepository;
using Management.Services.Utiliy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Net.Codecrete.QrCodeGenerator;
using Key = Management.Services.Utiliy.Key;

namespace Management.Services.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = SD.Admin + "," + SD.Staff)]
public class StudentsController : ControllerBase
{
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger _logger;

    private static string _fileName;
    private readonly IMapper _mapper;

    public StudentsController(
         IWebHostEnvironment webHostEnvironment,
         IUnitOfWork unitOfWork, 
         ILogger<StudentsController> logger,
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

             var students = GetStudentList(file.FileName);
             
             FileInfo fileNeedToDeleted = new FileInfo(fileName);
            
             if (fileNeedToDeleted.Exists)
             {  
                 fileNeedToDeleted.Delete();
             }  

             return Ok(_mapper.Map<List<StudentDto>>(students.Result));
         }
         catch (Exception exception)
         {
             _logger.LogInformation(exception.Message);
             return StatusCode(500, $"Internal server error: {exception.Message}");
         }
     }
    
    [HttpPost("[action]")]
    public async Task<IActionResult> Edit([FromBody] StudentDto studentDto)
    {
         var studentInDb = await _unitOfWork.Student.GetAsync(studentDto.Id);

         var doesStudentExists = await _unitOfWork.Student.GetAllAsync(e => e.StudentId == studentDto.StudentId && e.Email == studentDto.Email);

         if (doesStudentExists.Any() && studentInDb.StudentId != studentDto.StudentId)
         {
             ModelState.AddModelError("",$"Something went wrong went update the recored {studentDto.Name}");
             return BadRequest(ModelState);
         }

         studentInDb.StudentId = studentDto.StudentId;
         studentInDb.Name = studentDto.Name;
         studentInDb.Email = studentDto.Email;
         studentInDb.Avatar = studentDto.Avatar;
        
         _unitOfWork.Save();
                 
         return Ok();
        
    }

    private async Task<List<Student>> GetStudentList(string fName)
    {
         var errorCount = 0;
         var studentCount = 0;
         var students = new List<Student>();
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
                 var doesStudentExist = await _unitOfWork.Student.GetAllAsync(
                     s => s.StudentId == reader.GetValue(0).ToString()
                 );

                 if (doesStudentExist.Any())
                 {
                     errorCount++;
                 }
                 else
                 {
                     students.Add(new Student()
                     {
                         StudentId = reader.GetValue(0).ToString(),
                         Name = reader.GetValue(1).ToString(),
                         Email = reader.GetValue(2).ToString(),
                     });
                     studentCount++;
                 }
             }
             await _unitOfWork.Student.AddRangeAsync(students);
         }
         catch (Exception exception)
         {
             
         }
         
         if (errorCount > 0)
         {
             
         }
         _unitOfWork.Save();
       
         return students;
    }
     
    [HttpGet("[action]")]
    public IActionResult ListAllStudent()
    {
        var students = _unitOfWork.Student.GetAllAsync().Result;
         return Ok(_mapper.Map<List<StudentDto>>(students));
    }
     
    [HttpGet("[action]")]
    public async Task<IActionResult> CreateQrCode()
    {
         var listStudent = await _unitOfWork.Student.GetStudentsWithNullQr();
        
         foreach (var student in listStudent)
         {
             var studentId = Encrypt(student.StudentId);
             var borderWidth = Math.Clamp(3, 0, 999999);
             var qrCode = QrCode.EncodeText(studentId, QrCode.Ecc.Medium);
             byte[] qrcode = qrCode.ToPng(20, (int)borderWidth);
             student.Qr = string.Format("data:image/png;base64,{0}", Convert.ToBase64String(qrcode));
         }

         _unitOfWork.Student.UpdateListStudent(listStudent);
         
         _unitOfWork.Save();
         
         return NoContent();
    }

    [NonAction]
    private static string Encrypt(string clearText)
    {
         var EncryptionKey = Key.PrivateKey;
         var clearBytes = Encoding.Unicode.GetBytes(clearText);
         using (Aes encryptor = Aes.Create())
         {
             Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
             encryptor.Key = pdb.GetBytes(32);
             encryptor.IV = pdb.GetBytes(16);
             using (MemoryStream ms = new MemoryStream())
             {
                 using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                 {
                     cs.Write(clearBytes, 0, clearBytes.Length);
                     cs.Close();
                 }
                 clearText = Convert.ToBase64String(ms.ToArray());
             }
         }
         return clearText;
    }

    [HttpGet("[action]/{id:int}")]
    public async Task<IActionResult> GetStudentById(int id)
    {
         try
         {
             var student = await _unitOfWork.Student.GetAsync(id);
             
             if (student == null)
             {
                 return NotFound();
             }

             return Ok(_mapper.Map<StudentDto>(student));
         }
         catch (Exception exception)
         {
             _logger.LogInformation(exception.Message);
             
             return StatusCode(500, $"Internal server error: {exception.Message}");
         }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
         var student = await _unitOfWork.Student.GetAsync(id);

         if (student == null)
         {
             return NotFound();
         }
         
         await _unitOfWork.Student.RemoveAsync(student.Id);
         
         _unitOfWork.Save();
         
         return Ok();
    }
    
    [HttpPost("[action]")]
    public async Task<IActionResult> Create([FromBody] StudentCreateDto studentCreateDto)
    {
        var doesStudentExist = await _unitOfWork.Student.GetAllAsync(
            s => s.StudentId == studentCreateDto.StudentId
        );

        if (doesStudentExist.Any())
        {
            return BadRequest("User Already Exist");
        }

        await _unitOfWork.Student.AddAsync(new Student()
        {
            Name = studentCreateDto.Name,
            Email = studentCreateDto.Email,
            StudentId = studentCreateDto.StudentId,
            Avatar = studentCreateDto.Avatar 
        });

        _unitOfWork.Save();

        return Ok();
    }
}