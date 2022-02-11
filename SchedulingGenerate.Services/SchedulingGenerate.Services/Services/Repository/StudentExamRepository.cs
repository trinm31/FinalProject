using Microsoft.EntityFrameworkCore;
using SchedulingGenerate.Services.DbContext;
using SchedulingGenerate.Services.Models;
using SchedulingGenerate.Services.Services.IRepository;

namespace SchedulingGenerate.Services.Services.Repository;

public class StudentExamRepository: IStudentExamRepository
{
    private readonly DbContextOptions<ApplicationDbContext> _dbContext;
    public StudentExamRepository(DbContextOptions<ApplicationDbContext> dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Update(StudentCourse studentCourse, string oldStudentId, string oldCourseId)
    {
        await using var _db = new ApplicationDbContext(_dbContext);
        var objInDb = await _db.StudentCourses.FirstOrDefaultAsync(e => e.CourseId == oldCourseId || e.StudentId == oldStudentId);
        if (objInDb != null)
        {
            objInDb.CourseId = studentCourse.CourseId;
            objInDb.StudentId = studentCourse.StudentId;

            _db.StudentCourses.Update(objInDb);
        }

        _db.SaveChanges();
    }

    public async Task Create(StudentCourse studentCourse)
    {
        await using var _db = new ApplicationDbContext(_dbContext);
        var objInDb = await _db.StudentCourses.FirstOrDefaultAsync(e => e.CourseId == studentCourse.CourseId || e.StudentId == studentCourse.StudentId);
        if (objInDb != null)
        {
            _db.StudentCourses.Add(studentCourse);
        }

        _db.SaveChanges();
    }

    public async Task Delete(StudentCourse studentCourse)
    {
        await using var _db = new ApplicationDbContext(_dbContext);
        var objInDb = await _db.StudentCourses.FirstOrDefaultAsync(e => e.CourseId == studentCourse.CourseId || e.StudentId == studentCourse.StudentId);
        if (objInDb != null)
        {
            _db.StudentCourses.Remove(studentCourse);
        }

        _db.SaveChanges();
    }
}