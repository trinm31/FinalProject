using SchedulingGenerate.Services.Models;

namespace SchedulingGenerate.Services.Services.IRepository;

public interface IStudentExamRepository:IRepositoryAsync<StudentCourse>
{
    Task Update(StudentCourse studentExam);
}