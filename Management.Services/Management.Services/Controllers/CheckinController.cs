// using System.Security.Cryptography;
// using System.Text;
// using AutoMapper;
// using ClosedXML.Excel;
// using Management.Services.Dtos;
// using Management.Services.Models;
// using Management.Services.Services.IRepository;
// using Management.Services.Utiliy;
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Mvc;
//
// namespace Management.Services.Controllers;
//
// [Route("api/[controller]")]
// [ApiController]
// [Authorize(Roles = SD.Admin + "," + SD.Staff)]
// public class CheckinController : ControllerBase
// {
//     private readonly IUnitOfWork _unitOfWork;
//     private readonly IMapper _mapper;
//
//     public CheckinController(IUnitOfWork unitOfWork, IMapper mapper)
//     {
//         _unitOfWork = unitOfWork;
//         _mapper = mapper;
//     }
//
//     [HttpGet("[action]/{studentId}/{eventId:int}")]
//     public async Task<IActionResult> CheckIn(string studentId,int eventId)
//     {
//         var checkStdValid = await _unitOfWork.Student.GetFirstOrDefaultAsync(s => s.StudentId == studentId);
//
//         var examInDb = await _unitOfWork.Exam.GetAsync(eventId);
//
//         if (checkStdValid != null)
//         {
//             var studentEventValid = await _unitOfWork.StudentExam
//                 .GetAllAsync(s => s.StudentId == checkStdValid.Id && s.ExamId == eventId);
//
//             if (studentEventValid.Any())
//             {
//                 return Ok(new { success = false, valid = true, studentName = checkStdValid.Name, studentAva = checkStdValid.Avatar });
//             }
//
//             StudentExam userExamValid = new StudentExam()
//             {
//                 ExamId = examInDb.Id,
//                 StudentId = checkStdValid.Id,
//                 TimeStamp = DateTime.Now
//             };
//
//             await _unitOfWork.StudentExam.AddAsync(userExamValid);
//             _unitOfWork.Save();
//             return Ok(new { success = true, valid = true, studentName = checkStdValid.Name, studentAva = checkStdValid.Avatar });
//         }
//
//         if (studentId == null)
//         {
//             return Ok(new { success = false, valid = false });
//         }
//
//         if (studentId.Length % 4 != 0)
//         {
//             return Ok(new { success = false, valid = false });
//         }
//
//         var studentInDb = await _unitOfWork.Student.GetFirstOrDefaultAsync(s => s.StudentId == Decrypt(studentId));
//
//         if (studentInDb == null)
//         {
//             return Ok(new { success = false, valid = false });
//         }
//
//         var studentEvent = await _unitOfWork.StudentExam
//             .GetAllAsync(s => s.StudentId == studentInDb.Id && s.ExamId == eventId);
//
//         if (studentEvent.Any())
//         {
//             return Ok(new { success = false, valid = true, studentName = studentInDb.Name, studentAva = studentInDb.Avatar });
//         }
//
//         StudentExam studentExam = new StudentExam()
//         {
//             ExamId = examInDb.Id,
//             StudentId = studentInDb.Id,
//             TimeStamp = DateTime.Now
//         };
//         
//         await _unitOfWork.StudentExam.AddAsync(studentExam);
//         _unitOfWork.Save();
//
//         return Ok(new { success = true, valid = true, studentName = studentInDb.Name, studentAva = studentInDb.Avatar });
//     }
//
//     [HttpGet("[action]/{id:int}")]
//     public async Task<IActionResult> Report(int id)
//     {
//         try
//         {
//             var examInDb = await _unitOfWork.Exam.GetAsync(id);
//
//             var listStudentId = await _unitOfWork.StudentExam.GetStudentsIdInExam(id);
//             
//             var userExams = await _unitOfWork.StudentExam
//                 .GetAllAsync(
//                     e => listStudentId.Any(stdId => stdId.Equals(e.StudentId)),
//                     q => q.OrderBy(t => t.TimeStamp),
//                     "Student,Event");
//
//             StudentExamReportDto studentExamReportDto = new StudentExamReportDto
//             {
//                 Exam = _mapper.Map<ExamDto>(examInDb),
//                 StudentExams = _mapper.Map<List<StudentExamDto>>(userExams)
//             };
//
//             return Ok(studentExamReportDto);
//         }
//         catch (Exception exception)
//         {
//             return StatusCode(500, $"Internal server error: {exception.Message}");
//         }
//     }
//
//     [HttpPost("[action]")]
//     public static string Decrypt([FromBody] string cipherText)
//     {
//         try
//         {
//             string EncryptionKey = Key.PrivateKey;
//             cipherText = cipherText.Replace(" ", "+");
//             byte[] cipherBytes = Convert.FromBase64String(cipherText);
//             using (Aes encryptor = Aes.Create())
//             {
//                 Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
//                 encryptor.Key = pdb.GetBytes(32);
//                 encryptor.IV = pdb.GetBytes(16);
//                 using (MemoryStream ms = new MemoryStream())
//                 {
//                     using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
//                     {
//                         cs.Write(cipherBytes, 0, cipherBytes.Length);
//                         cs.Close();
//                     }
//                     cipherText = Encoding.Unicode.GetString(ms.ToArray());
//                 }
//             }
//             return cipherText;
//         }
//         catch (Exception)
//         {
//             return "StudentWrong";
//         }
//     }
//
//     [HttpGet("[action]/{id:int}")]
//     public async Task<IActionResult> Excel(int id)
//     {
//         var listStudentId = await _unitOfWork.StudentExam.GetStudentsIdInExam(id);
//
//         var userEvents = await _unitOfWork.StudentExam
//             .GetAllAsync(
//                 e => listStudentId.Any(stdId => stdId.Equals(e.StudentId)),
//                 q => q.OrderBy(t => t.TimeStamp),
//                 "Student,Event");
//
//         using (var workbook = new XLWorkbook())
//         {
//             var worksheet = workbook.Worksheets.Add("Student");
//             var currentRow = 1;
//             worksheet.Cell(currentRow, 1).Value = "No.";
//             worksheet.Cell(currentRow, 2).Value = "Student Id";
//             worksheet.Cell(currentRow, 3).Value = "Student Name";
//
//             foreach (var student in userEvents)
//             {
//                 currentRow++;
//                 worksheet.Cell(currentRow, 1).Value = currentRow - 1;
//                 worksheet.Cell(currentRow, 2).Value = student.Student.StudentId;
//                 worksheet.Cell(currentRow, 3).Value = student.Student.Name;
//             }
//
//             await using (var stream = new MemoryStream())
//             {
//                 workbook.SaveAs(stream);
//                 var content = stream.ToArray();
//                 return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "student.xlsx");
//             }
//         }
//     }
// }