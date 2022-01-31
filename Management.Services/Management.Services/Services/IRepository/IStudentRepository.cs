using Management.Services.Models;

namespace Management.Services.Services.IRepository;

public interface IStudentRepository: IRepositoryAsync<Student>
{
    Task<List<Student>> GetStudentsWithNullQr();
    Task<int> CountStudentHasQr();
    void UpdateListStudent(List<Student> students);
    Task<bool> CheckExistStudent(string studentId);
}