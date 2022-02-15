using SchedulingGenerate.Services.Messages;
using SchedulingGenerate.Services.Models;

namespace SchedulingGenerate.Services.Services.IRepository;

public interface ISettingRepository
{
    Task Update(SchedulingSettingRequestDto setting);
}