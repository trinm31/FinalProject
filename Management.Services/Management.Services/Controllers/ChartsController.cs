using AutoMapper;
using Management.Services.DbContext;
using Management.Services.Services.IRepository;
using Management.Services.Utiliy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Management.Services.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = SD.Admin)]
public class ChartsController : ControllerBase
{
    private readonly ApplicationDbContext _db;

    public ChartsController(ApplicationDbContext db)
    {
        _db = db;
    }

    [HttpGet("[action]")]
    public IActionResult Index()
    {
        var numberOfStudent = _db.Students.ToList().Count();
        var numberOfStudentInCourse = _db.StudentExams.ToList().Count();
        var numberOfCourse = _db.Exams.ToList().Count();
        var result = new int[3]{numberOfStudent, numberOfStudentInCourse, numberOfCourse};
        return Ok(result);
    }
}