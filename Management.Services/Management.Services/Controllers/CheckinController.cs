using System.Security.Cryptography;
using System.Text;
using System.Web;
using Abp.Extensions;
using AutoMapper;
using ClosedXML.Excel;
using Management.Services.DbContext;
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
public class CheckinController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ApplicationDbContext _db;

    public CheckinController(IUnitOfWork unitOfWork, IMapper mapper, ApplicationDbContext db)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _db = db;
    }

    [HttpGet("[action]/{studentId}/{roomId:int}")]
    public async Task<IActionResult> CheckInConfirm(string studentId,int roomId)
    {
        if (studentId == null || roomId == null)
        {
            return Ok(new { success = false, data = "StudentId or RoomId is null" });
        }

        var studentIdEncode = Decrypt(HttpUtility.UrlDecode(studentId));
        
        var studentInDb = await _unitOfWork.Student.GetFirstOrDefaultAsync(s => s.StudentId == studentIdEncode);

        var roomInDb = _db.Rooms.Find(roomId);

        if (studentInDb != null && roomInDb != null)
        {
            var studentRoomValid = _db.Checkins.Where(x => x.StudentId == studentIdEncode && x.RoomId == roomId);

            if (studentRoomValid.Any())
            {
                return Ok(new { success = true, studentId = studentInDb.StudentId ,studentName = studentInDb.Name, studentAva = studentInDb.Avatar });
            }

            _db.Checkins.Add(new Checkin()
            {
                RoomId = roomId,
                StudentId = studentIdEncode
            });
            
            _db.SaveChanges();
            
            return Ok(new { success = true, studentId = studentInDb.StudentId ,studentName = studentInDb.Name, studentAva = studentInDb.Avatar });
        }

        return Ok(new { success = false, data = "StudentId or RoomId is invalid" });
    }
    
    [HttpGet("[action]/{studentId}")]
    [AllowAnonymous]
    public async Task<IActionResult> CheckIn(string studentId)
    {
        var studentInDb = await _unitOfWork.Student.GetFirstOrDefaultAsync(s => s.StudentId == Decrypt(HttpUtility.UrlDecode(studentId)));

        if (studentInDb == null)
        {
            return NotFound();
        }
        return Ok(new { studentId = studentInDb.StudentId ,studentName = studentInDb.Name, studentAva = studentInDb.Avatar });
    }
    
    private string Decrypt(string cipherText)
    {
        try
        {
            string EncryptionKey = Key.PrivateKey;
            cipherText = cipherText.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }
        catch (Exception)
        {
            return "StudentWrong";
        }
    }

    [HttpGet("[action]/{id:int}")]
    public async Task<IActionResult> Excel(int roomId)
    {
        var listStudentId = _db.Checkins.Where(x=>x.RoomId == roomId);
        var studentList =
            _db.Students.Where(x => x.StudentId.IsIn(listStudentId.Select(x => x.StudentId).ToArray()));
    
        using (var workbook = new XLWorkbook())
        {
            var worksheet = workbook.Worksheets.Add("Student");
            var currentRow = 1;
            worksheet.Cell(currentRow, 1).Value = "No.";
            worksheet.Cell(currentRow, 2).Value = "Student Id";
            worksheet.Cell(currentRow, 3).Value = "Student Name";
    
            foreach (var student in studentList)
            {
                currentRow++;
                worksheet.Cell(currentRow, 1).Value = currentRow - 1;
                worksheet.Cell(currentRow, 2).Value = student.StudentId;
                worksheet.Cell(currentRow, 3).Value = student.Name;
            }
    
            await using (var stream = new MemoryStream())
            {
                workbook.SaveAs(stream);
                var content = stream.ToArray();
                return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "student.xlsx");
            }
        }
    }
    
    [HttpGet("[action]/{id:int}")]
    public async Task<IActionResult> Report(int roomId)
    {
        try
        {
            var roomInDb = _db.Rooms.Find(roomId);
            if (roomInDb != null)
            {
                var listStudentId = _db.Checkins.Where(x=>x.RoomId == roomId);
                var studentList =
                    _db.Students.Where(x => x.StudentId.IsIn(listStudentId.Select(x => x.StudentId).ToArray()));

                return Ok(studentList);
            }

            return NotFound();
        }
        catch (Exception exception)
        {
            return StatusCode(500, $"Internal server error: {exception.Message}");
        }
    }
}