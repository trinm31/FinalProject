using Management.Services.DbContext;
using Management.Services.Models;
using Management.Services.Services.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Management.Services.Services.Repository;

public class ExamRepository: RepositoryAsync<Exam>,IExamRepository
{
    private readonly ApplicationDbContext _db;

    public ExamRepository(ApplicationDbContext db) : base(db)
    {
        this._db = db;
    }

    public async Task Update(Exam exam)
    {
        var objInDb = await _db.Exams.FirstOrDefaultAsync(e => e.Id == exam.Id);
        if (objInDb != null)
        {
            if (!await CheckExistExam(exam))
            {
                objInDb.Name = exam.Name;
                objInDb.Description = exam.Description;
            }

            _db.Exams.Update(objInDb);
        }
    }
    
    public async Task<bool> ChangeStatusExam(int id)
    {
        var exam = await GetAsync(id);
        if (exam == null)
        {
            return false;
        }
        exam.Status = exam.Status != true;
        return true;
    }

    public async Task<bool> CheckExistExam(Exam exam)
    {
        return await _db.Exams.AnyAsync(e => e.Name.Contains(exam.Name) && e.Id != exam.Id);
    }
}