using SchedulingGenerate.Services.Models;

namespace SchedulingGenerate.Services.Services.IRepository;

public interface IStudentRepository: IRepositoryAsync<Student>
{
    void UpdateListStudent(List<Student> students);
    void Update(Student student);
}