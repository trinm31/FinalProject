using Management.Services.DbContext;
using Management.Services.Models;
using Management.Services.Services.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Management.Services.Services.Repository;

public class RoomRepository: IRoomRepository
{
    
    private readonly DbContextOptions<ApplicationDbContext> _dbContext;

    public RoomRepository(DbContextOptions<ApplicationDbContext> dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Room>> GetAll()
    {
        await using var _db = new ApplicationDbContext(_dbContext);
        var rooms = _db.Rooms.ToList();
        return rooms;
    }

    public async Task Update(Room room)
    {
        await using var _db = new ApplicationDbContext(_dbContext);
        var roomDb = _db.Rooms.Find(room.Id);
        if (roomDb != null)
        {
            var nameCheck = _db.Rooms.Where(r => r.Name == room.Name && r.Id != room.Id).ToList();
            if (nameCheck == null)
            {
                _db.Rooms.Update(room);
                _db.SaveChanges();
            }
        }
    }

    public async Task<Room> Read(int id)
    {
        await using var _db = new ApplicationDbContext(_dbContext);
        var result = _db.Rooms.Find(id);
        return result;
    }

    public async Task Create(Room room)
    {
        await using var _db = new ApplicationDbContext(_dbContext);
        var roomDb = _db.Rooms.Find(room.Id);
        if (roomDb != null)
        {
            var nameCheck = _db.Rooms.Where(r => r.Name == room.Name).ToList();
            if (nameCheck == null)
            {
                _db.Rooms.Add(room);
                _db.SaveChanges();
            }
        }
    }

    public async Task Delete(int id)
    {
        await using var _db = new ApplicationDbContext(_dbContext);
        var roomDb = _db.Rooms.Find(id);
        if (roomDb != null)
        {
            _db.Rooms.Remove(roomDb);
            _db.SaveChanges();
        }
    }
}