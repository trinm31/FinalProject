using System.Drawing;
using System.Security.Cryptography;
using System.Text;
using ExcelDataReader;
using Management.Services.Models;
using Management.Services.Services.IRepository;
using Management.Services.Utiliy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QRCoder;
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

    public StudentsController(
         IWebHostEnvironment webHostEnvironment,
         IUnitOfWork unitOfWork, 
         ILogger<StudentsController> logger
     )
    {
         _webHostEnvironment = webHostEnvironment;
         _unitOfWork = unitOfWork;
         _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> Upload()
     {
         try
         {
             var file = Request.Form.Files[0];
             if (file == null)
             {
                 return BadRequest();
             }
             var separator = Path.DirectorySeparatorChar;
             string fileName = $"{_webHostEnvironment.WebRootPath}{separator}files{separator}{file.FileName}";

             _fileName = file.FileName.Split(".")[0];

             await using (FileStream fileStream = System.IO.File.Create(fileName))
             {
                 await file.CopyToAsync(fileStream);
                 fileStream.Flush();
             }

             var students = GetStudentList(file.FileName);
             return Ok(students.Result);
         }
         catch (Exception exception)
         {
             _logger.LogInformation(exception.Message);
             return StatusCode(500, $"Internal server error: {exception.Message}");
         }
     }
    
    [HttpGet("[action]/{id:int}")]
    public async Task<IActionResult> Edit(int id)
    {
         var student = await _unitOfWork.Student.GetFirstOrDefaultAsync(s => s.Id == id);
         if (student == null)
         {
             return BadRequest();
         }
         return Ok(student); 
    }

    [HttpPost] 
    public async Task<IActionResult> Edit([FromBody]Student student)
    {
        var files = HttpContext.Request.Form.Files;

         var studentInDb = await _unitOfWork.Student.GetAsync(student.Id);

         var doesStudentExists = await _unitOfWork.Student.GetAllAsync(e => e.StudentId == student.StudentId);

         if (doesStudentExists.Any() && studentInDb.StudentId != student.StudentId)
         {
             return Ok(student);
         }

         studentInDb.StudentId = student.StudentId;
         studentInDb.Name = student.Name;

         if (files.Count > 0)
         {
             if (ModelState.IsValid)
             {
                 using (var memoryStream = new MemoryStream())
                 {
                     await files[0].CopyToAsync(memoryStream);

                     // Upload the file if less than 6 MB
                     if (memoryStream.Length < 6291456)
                     {
                         studentInDb.Avatar = memoryStream.ToArray();
                     }
                     else
                     {
                         ModelState.AddModelError("File", "The file is too large.");
                     }
                 }

                 _unitOfWork.Save();
                 
                 return Ok();
             }
         }
         return Ok("Invalid");
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
         return Ok(_unitOfWork.Student.GetAllAsync().Result);
     }
     
     [HttpPost]
     public async Task<IActionResult> CreateQrCode()
     {
         var listStudent = await _unitOfWork.Student.GetStudentsWithNullQr();
         using (QRCodeGenerator qrCodeGenerator = new QRCodeGenerator())
         {
             if (!listStudent.Any())
             {
                 return StatusCode(404, ModelState);
             }
             else
             {
                 await _unitOfWork.Download.AddNewDownload(_fileName);
             }

             var num = await _unitOfWork.Student.CountStudentHasQr();
             
             foreach (var student in listStudent)
             {
                 var studentId = Encrypt(student.StudentId);
                 using QRCodeData qrCodeData = qrCodeGenerator.CreateQrCode(studentId, QRCodeGenerator.ECCLevel.Q);
                 Bitmap qrCodeImage;
                 using (QRCode qrCode = new QRCode(qrCodeData))
                 {
                     qrCodeImage = qrCode.GetGraphic(20);
                 }
                 await BitmapToBytesCode(qrCodeImage, student, num);
                 num++;
             }
         }
         
         _unitOfWork.Student.UpdateListStudent(listStudent);
         
         _unitOfWork.Save();
         
         return NoContent();
     }

     [NonAction]
     private async Task BitmapToBytesCode(Bitmap image, Student student, int num)
     {
         var webRootPath = _webHostEnvironment.WebRootPath;
         
         await using var stream = new MemoryStream();
         
         image.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
         
         var img = Image.FromStream(stream);
         
         var qrPath = "/qr/" + $"{StudentsController._fileName}-at-{DateTime.Now.Day}-{DateTime.Now.Month}-{DateTime.Now.Year}/";
         
         var zipPath = webRootPath + qrPath;
         
         if (!Directory.Exists(zipPath))
             Directory.CreateDirectory(zipPath);
         
         student.Qr = stream.ToArray();
         
         img.Save(zipPath + num.ToString() + "." + student.Name + ".png", System.Drawing.Imaging.ImageFormat.Png);
         
         stream.ToArray();
         
         stream.Close();
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
     public async Task<IActionResult> Details(int id)
     {
         try
         {
             var student = await _unitOfWork.Student.GetAsync(id);
             
             if (student == null)
             {
                 return NotFound();
             }

             return Ok(student);
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
}