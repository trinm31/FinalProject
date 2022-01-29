namespace Management.Services.Services.IRepository;

public interface IUnitOfWork: IDisposable
{
    public IExamRepository Exam { get; }
    public IStudentRepository Student { get; }
    public IDownloadRepository Download { get; }
    public IStudentExamRepository StudentExam { get; }
    void Save();
}