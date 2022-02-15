using Microsoft.EntityFrameworkCore;
using SchedulingGenerate.Services.DbContext;
using SchedulingGenerate.Services.Messages;
using SchedulingGenerate.Services.Models;
using SchedulingGenerate.Services.Services.IRepository;

namespace SchedulingGenerate.Services.Services.Repository;

public class SettingRepository:ISettingRepository
{
    private readonly DbContextOptions<ApplicationDbContext> _dbContext;

    public SettingRepository(DbContextOptions<ApplicationDbContext> dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task Update(SchedulingSettingRequestDto setting)
    {
        await using var _db = new ApplicationDbContext(_dbContext);
        var settingdb = await _db.Settings.FirstAsync();
        
        settingdb.StartDate = setting.StartDate;
        settingdb.EndDate = setting.EndDate;
        settingdb.ConcurrencyLevelDefault = setting.ConcurrencyLevelDefault;
        settingdb.NoOfTimeSlot = setting.NoOfTimeSlot;
        settingdb.InternalDistance = setting.InternalDistance;
        settingdb.ExternalDistance = setting.ExternalDistance;

        _db.Settings.Update(settingdb);
        _db.SaveChanges();
    }
}