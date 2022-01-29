using Management.Services.Models;

namespace Management.Services.Services.IRepository;

public interface IExamRepository: IRepositoryAsync<Exam>
{
    Task Update(Exam exam);
    Task<bool> ChangeStatusExam(int id);
    Task<bool> CheckExistExam(string name);
}