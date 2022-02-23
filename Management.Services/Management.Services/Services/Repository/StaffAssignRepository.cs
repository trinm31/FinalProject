using Management.Services.DbContext;
using Management.Services.Models;
using Management.Services.Services.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Management.Services.Services.Repository;

public class StaffAssignRepository: IStaffAssignRepository
{
    private readonly DbContextOptions<ApplicationDbContext> _dbContext;

    public StaffAssignRepository(DbContextOptions<ApplicationDbContext> dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task Assign(string staffId, int roomId)
    {
        await using var _db = new ApplicationDbContext(_dbContext);
        var checkAssigned = _db.StaffAssigns.Where(x => x.StaffId == staffId && x.RoomId == roomId);
        if (!checkAssigned.Any())
        {
            _db.Add(new StaffAssign() { RoomId = roomId, StaffId = staffId });
            _db.SaveChanges();
        }
    }

    public async Task UnAssign(string staffId, int roomId)
    {
        await using var _db = new ApplicationDbContext(_dbContext);
        var checkAssigned = _db.StaffAssigns.Where(x => x.StaffId == staffId && x.RoomId == roomId);
        if (checkAssigned.Any())
        {
            _db.Remove(checkAssigned);
            _db.SaveChanges();
        }
    }
}