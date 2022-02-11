using SchedulingGenerate.Services.Models;

namespace SchedulingGenerate.Services.Services.IRepository;

public interface IExamRepository: IRepositoryAsync<Course>
{
    Task Update(Course course);
}