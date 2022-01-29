using Management.Services.Models;

namespace Management.Services.Services.IRepository;

public interface IDownloadRepository:IRepositoryAsync<Download>
{
    Task AddNewDownload(string fileName);
}