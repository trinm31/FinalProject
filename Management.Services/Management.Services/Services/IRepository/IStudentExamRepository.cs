using Management.Services.Models;

namespace Management.Services.Services.IRepository;

public interface IStudentExamRepository:IRepositoryAsync<StudentExam>
{
    Task<string[]> GetStudentsIdInExam(string id);
    Task<bool> CheckExistStudentExam(StudentExam studentExam);
    Task Update(StudentExam studentExam);
}