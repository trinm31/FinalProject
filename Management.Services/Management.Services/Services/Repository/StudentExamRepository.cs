using Management.Services.DbContext;
using Management.Services.Models;
using Management.Services.Services.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Management.Services.Services.Repository;

public class StudentExamRepository:RepositoryAsync<StudentExam>, IStudentExamRepository
{
    private readonly ApplicationDbContext _db;
    public StudentExamRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }

    public async Task<string[]> GetStudentsIdInExam(string id)
    {
        var listStudentId = await _db.StudentExams
            .Where(e => e.ExamId == id)
            .Select(s => s.StudentId).ToArrayAsync();
        return listStudentId; 
    }

    public async Task<bool> CheckExistStudentExam(StudentExam studentExam)
    {
        var studentCheck = await _db.Students.AnyAsync(e => e.StudentId.Contains(studentExam.StudentId));
        var examCheck = await _db.Exams.AnyAsync(e => e.ExamId.Contains(studentExam.ExamId));

        return !(studentCheck && examCheck);
    }

    public async Task Update(StudentExam studentExam)
    {
        var objInDb = await _db.StudentExams.FirstOrDefaultAsync(e => e.Id == studentExam.Id);
        if (objInDb != null)
        {
            if (!await CheckExistStudentExam(studentExam))
            {
                objInDb.ExamId = studentExam.ExamId;
                objInDb.StudentId = studentExam.StudentId;
            }

            _db.StudentExams.Update(objInDb);
        }
    }
}