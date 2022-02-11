using SchedulingGenerate.Services.Models;

namespace SchedulingGenerate.Services.Services.IRepository;

public interface IExamRepository
{
    Task Update(Course course);
    Task Create(Course course);
    Task Delete(Course course);
}