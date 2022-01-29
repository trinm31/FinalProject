using Management.Services.DbContext;
using Management.Services.Models;
using Management.Services.Services.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Management.Services.Services.Repository;

public class StudentRepository : RepositoryAsync<Student>, IStudentRepository
{
    private readonly ApplicationDbContext _db;
    public StudentRepository(ApplicationDbContext db ) : base(db)
    {
        _db = db;
    }

    public async Task<int> CountStudentHasQr()
    {
        var countStudent = await _db.Students.CountAsync(s => s.Qr != null);
        return countStudent;
    }

    public void UpdateListStudent(List<Student> students)
    {
        _db.Students.UpdateRange(students);
    }

    public async Task<bool> CheckExistStudent(string studentId)
    {
        return await _db.Students.AnyAsync(e => e.Name.Contains(studentId));
    }
    
    public async Task<List<Student>> GetStudentsWithNullQr(int numberOfStudent)
    {
        var students = await _db.Students.
            Where(s => s.Qr == null).
            Take(numberOfStudent).
            ToListAsync();
        return students;
    }
}