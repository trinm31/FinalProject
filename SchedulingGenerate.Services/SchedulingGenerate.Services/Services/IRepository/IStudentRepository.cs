using SchedulingGenerate.Services.Models;

namespace SchedulingGenerate.Services.Services.IRepository;

public interface IStudentRepository
{
    Task Update(Student student,string oldStudentId);
    Task Create(Student student);
    Task Delete(Student student);
}