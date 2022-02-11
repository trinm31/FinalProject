using Management.Services.MessageBus;

namespace Management.Services.Messages;

public class SchedulingCrudStudentCourseMessage: BaseMessage
{
    public string MethodType { get; set; }
    public int Id { get; set; }
    public string CourseId { get; set; }
    public string StudentId { get; set; }
    public string oldCourseId { get; set; }
    public string oldStudentId { get; set; }
}