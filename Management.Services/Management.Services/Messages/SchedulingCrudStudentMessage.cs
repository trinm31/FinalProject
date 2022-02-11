using Management.Services.MessageBus;

namespace Management.Services.Messages;

public class SchedulingCrudStudentMessage:BaseMessage
{
    public string MethodType { get; set; }
    public string Id { get; set; }
    public string Name { get; set; }
    public string oldStudentId { get; set; }
}