using Management.Services.DbContext;
using Management.Services.Models;
using Management.Services.Services.IRepository;

namespace Management.Services.Services.Repository;

public class DownloadRepository : RepositoryAsync<Download>, IDownloadRepository
{
    private readonly ApplicationDbContext _db;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public DownloadRepository(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment) : base(db)
    {
        _db = db;
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task AddNewDownload(string fileName)
    {
     
        await _db.Downloads.AddAsync(new Download()
        {
            Name = $"{fileName}",
            ZipPath = $"{fileName}-at-{DateTime.Now.Day}-{DateTime.Now.Month}-{DateTime.Now.Year}"
        });
    }
}