namespace SchedulingGenerate.Services.Services.IRepository;

public interface IUnitOfWork: IDisposable
{
    public IExamRepository Exam { get; }
    public IStudentRepository Student { get; }
    public IStudentExamRepository StudentExam { get; }
    void Save();
}