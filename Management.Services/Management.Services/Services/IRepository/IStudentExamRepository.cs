using Management.Services.Models;

namespace Management.Services.Services.IRepository;

public interface IStudentExamRepository:IRepositoryAsync<StudentExam>
{
    Task<int[]> GetStudentsIdInExam(int id);
}