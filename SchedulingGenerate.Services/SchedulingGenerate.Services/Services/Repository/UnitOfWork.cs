
using SchedulingGenerate.Services.DbContext;
using SchedulingGenerate.Services.Services.IRepository;

namespace SchedulingGenerate.Services.Services.Repository;

public class UnitOfWork: IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UnitOfWork(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            Exam = new ExamRepository(_db);
            Student = new StudentRepository(_db);
            StudentExam = new StudentExamRepository(_db);
            _webHostEnvironment = webHostEnvironment;
        }
        public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
            this.Dispose();
        }
        
        public IStudentRepository Student { get; }
        public IStudentExamRepository StudentExam { get; }
        public IExamRepository Exam { get; private set; }
        
    }