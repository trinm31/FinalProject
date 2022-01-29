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

    public async Task<int[]> GetStudentsIdInExam(int id)
    {
        var listStudentId = await _db.StudentExams
            .Where(e => e.ExamId == id)
            .Select(s => s.StudentId).ToArrayAsync();
        return listStudentId; 
    }
}