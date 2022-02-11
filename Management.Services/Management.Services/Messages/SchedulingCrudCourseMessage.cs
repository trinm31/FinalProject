using Management.Services.MessageBus;

namespace Management.Services.Messages;

public class SchedulingCrudCourseMessage:BaseMessage
{
    public string MethodType { get; set; }
    public string Id { get; set; }
    public string Name { get; set; }
}