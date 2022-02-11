using Microsoft.EntityFrameworkCore;
using SchedulingGenerate.Services.DbContext;
using SchedulingGenerate.Services.Models;
using SchedulingGenerate.Services.Services.IRepository;

namespace SchedulingGenerate.Services.Services.Repository;

public class ExamRepository: IExamRepository
{
    private readonly DbContextOptions<ApplicationDbContext> _dbContext;

    public ExamRepository(DbContextOptions<ApplicationDbContext> dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Update(Course course)
    {
        await using var _db = new ApplicationDbContext(_dbContext);
        var objInDb = await _db.Courses.FirstOrDefaultAsync(e => e.ExamId == course.ExamId);
        if (objInDb != null)
        {
            objInDb.ExamId = course.Name;
            objInDb.Name = course.Name;
            _db.Courses.Update(objInDb);
        }

        _db.SaveChanges();
    }

    public async Task Create(Course course)
    {
        await using var _db = new ApplicationDbContext(_dbContext);
        var objInDb = await _db.Courses.FirstOrDefaultAsync(e => e.ExamId == course.ExamId);
        if (objInDb == null)
        {
            _db.Courses.Add(course);
        }

        _db.SaveChanges();
    }

    public async Task Delete(Course course)
    {
        await using var _db = new ApplicationDbContext(_dbContext);
        var objInDb = await _db.Courses.FirstOrDefaultAsync(e => e.ExamId == course.ExamId);
        if (objInDb != null)
        {
            _db.Courses.Remove(objInDb);
        }

        _db.SaveChanges();
    }
}