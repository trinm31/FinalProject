namespace Management.Services.Services.IRepository;

public interface IStaffAssignRepository
{
    Task Assign(string staffId, int roomId);
    Task UnAssign(string staffId, int roomId);
}