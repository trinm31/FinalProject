using Management.Services.MessageBus;

namespace Management.Services.Messages;

public class SchedulingCreateStudentCourseMessage: BaseMessage
{
    public List<StudentCourse> StudentCourses { get; set; }
}