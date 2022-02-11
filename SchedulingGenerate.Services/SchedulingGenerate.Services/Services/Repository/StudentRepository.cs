using System.Linq;
using Microsoft.EntityFrameworkCore;
using SchedulingGenerate.Services.DbContext;
using SchedulingGenerate.Services.Models;
using SchedulingGenerate.Services.Services.IRepository;

namespace SchedulingGenerate.Services.Services.Repository;

public class StudentRepository : IStudentRepository
{
    private readonly DbContextOptions<ApplicationDbContext> _dbContext;
    public StudentRepository(DbContextOptions<ApplicationDbContext> dbContext)
    {
        _dbContext = dbContext;
    }
    

    public async Task Create(Student student)
    {
        await using var _db = new ApplicationDbContext(_dbContext);
        var objInDb = await _db.Students.FirstOrDefaultAsync(e => e.StudentId == student.StudentId);
        if (objInDb == null)
        {
            _db.Students.Add(student);
        }

        _db.SaveChanges();
    }

    public async Task Delete(Student student)
    {
        await using var _db = new ApplicationDbContext(_dbContext);
        var objInDb = await _db.Students.FirstOrDefaultAsync(e => e.StudentId == student.StudentId);
        if (objInDb == null)
        {
            _db.Students.Remove(objInDb);
        }

        _db.SaveChanges();
    }

    public async Task Update(Student student , string oldStudentId)
    {
        await using var _db = new ApplicationDbContext(_dbContext);
        var objInDb = await _db.Students.FirstOrDefaultAsync(e => e.StudentId == oldStudentId);
        if (objInDb == null)
        {
            objInDb.StudentId = student.StudentId;
            objInDb.Name = student.Name;
            _db.Students.Update(objInDb);
        }

        _db.SaveChanges();
    }
}