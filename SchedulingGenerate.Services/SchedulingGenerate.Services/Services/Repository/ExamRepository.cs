using Microsoft.EntityFrameworkCore;
using SchedulingGenerate.Services.DbContext;
using SchedulingGenerate.Services.Models;
using SchedulingGenerate.Services.Services.IRepository;

namespace SchedulingGenerate.Services.Services.Repository;

public class ExamRepository: RepositoryAsync<Course>,IExamRepository
{
    private readonly ApplicationDbContext _db;

    public ExamRepository(ApplicationDbContext db) : base(db)
    {
        this._db = db;
    }

    public async Task Update(Course course)
    {
        var objInDb = await _db.Courses.FirstOrDefaultAsync(e => e.Id == course.Id);
        if (objInDb != null)
        {
            objInDb.Id = course.Id;
            objInDb.Name = course.Name;

            _db.Courses.Update(objInDb);
        }
    }
    
}