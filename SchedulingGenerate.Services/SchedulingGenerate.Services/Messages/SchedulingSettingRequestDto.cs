using SchedulingGenerate.Services.Models;

namespace SchedulingGenerate.Services.Messages;

public class SchedulingSettingRequestDto
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int ConcurrencyLevelDefault { get; set; }
    public int InternalDistance { get; set; }
    public int ExternalDistance { get; set; }
    public int NoOfTimeSlot { get; set; }
}