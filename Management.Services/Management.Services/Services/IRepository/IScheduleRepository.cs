using Management.Services.Dtos;
using Management.Services.Models;

namespace Management.Services.Services.IRepository;

public interface IScheduleRepository
{
    Task CreateRange(List<Schedule> schedules);
    Task DeleteAll();
    Task<ScheduleDto> GetAll();
}