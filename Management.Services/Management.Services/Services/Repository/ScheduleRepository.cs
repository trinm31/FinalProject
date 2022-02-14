using Management.Services.DbContext;
using Management.Services.Models;
using Management.Services.Services.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Management.Services.Services.Repository;

public class ScheduleRepository : IScheduleRepository
{
    private readonly DbContextOptions<ApplicationDbContext> _dbContext;

    public ScheduleRepository(DbContextOptions<ApplicationDbContext> dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task CreateRange(List<Schedule> schedules)
    {
        await using var _db = new ApplicationDbContext(_dbContext);
        _db.AddRange(schedules);
        _db.SaveChanges();
    }

    public async Task DeleteAll()
    {
        await using var _db = new ApplicationDbContext(_dbContext);
        var schedules = _db.Schedules.ToList();
        _db.RemoveRange(schedules);
        _db.SaveChanges();
    }
}