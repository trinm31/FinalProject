using SchedulingGenerate.Services.Models;

namespace SchedulingGenerate.Services.Services.IRepository;

public interface IStudentExamRepository
{
    Task Update(StudentCourse studentCourse, string oldStudentId, string oldCourseId);
    Task Create(StudentCourse studentCourse);
    Task Delete(StudentCourse studentCourse);
}