using SchedulingGenerate.Services.MessageBus;
using SchedulingGenerate.Services.Models;

namespace SchedulingGenerate.Services.Messages;

public class SchedulingResultMessage: BaseMessage
{
    public string MethodType { get; set; }
    public List<Result> ResultList { get; set; }
}