using Microsoft.EntityFrameworkCore;
using SchedulingGenerate.Services.DbContext;
using SchedulingGenerate.Services.Models;
using SchedulingGenerate.Services.Services.IRepository;

namespace SchedulingGenerate.Services.Services.Repository;

public class StudentExamRepository:RepositoryAsync<StudentCourse>, IStudentExamRepository
{
    private readonly ApplicationDbContext _db;
    public StudentExamRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }

    public async Task Update(StudentCourse studentExam)
    {
        var objInDb = await _db.StudentCourses.FirstOrDefaultAsync(e => e.Id == studentExam.Id);
        if (objInDb != null)
        {
            objInDb.CourseId = studentExam.CourseId;
            objInDb.StudentId = studentExam.StudentId;

            _db.StudentCourses.Update(objInDb);
        }
    }
}