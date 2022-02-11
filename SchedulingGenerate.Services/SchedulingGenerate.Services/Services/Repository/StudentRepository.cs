using System.Linq;
using Microsoft.EntityFrameworkCore;
using SchedulingGenerate.Services.DbContext;
using SchedulingGenerate.Services.Models;
using SchedulingGenerate.Services.Services.IRepository;

namespace SchedulingGenerate.Services.Services.Repository;

public class StudentRepository : RepositoryAsync<Student>, IStudentRepository
{
    private readonly ApplicationDbContext _db;
    public StudentRepository(ApplicationDbContext db ) : base(db)
    {
        _db = db;
    }

    public void UpdateListStudent(List<Student> students)
    {
        _db.Students.UpdateRange(students);
    }

    public void Update(Student student)
    {
        _db.Students.UpdateRange(student);
    }
}