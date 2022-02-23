using Management.Services.Models;

namespace Management.Services.Services.IRepository;

public interface IRoomRepository
{
    Task<IEnumerable<Room>> GetAll();
    Task Update(Room room);
    Task<Room> Read(int id);
    Task Create(Room room);
    Task Delete(int id);
    Task CreateRange(List<Room> rooms);
}