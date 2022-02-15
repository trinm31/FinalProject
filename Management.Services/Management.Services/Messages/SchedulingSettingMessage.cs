using Management.Services.MessageBus;
using Management.Services.Migrations;

namespace Management.Services.Messages;

public class SchedulingSettingMessage:BaseMessage
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int ConcurrencyLevelDefault { get; set; }
    public int InternalDistance { get; set; }
    public int ExternalDistance { get; set; }
    public int NoOfTimeSlot { get; set; }
}