using Management.Services.MessageBus;
using Management.Services.Migrations;

namespace Management.Services.Messages;

public class SchedulingResultRequestDto
{
    public string MethodType { get; set; }
    public List<Result> ResultList { get; set; }
}