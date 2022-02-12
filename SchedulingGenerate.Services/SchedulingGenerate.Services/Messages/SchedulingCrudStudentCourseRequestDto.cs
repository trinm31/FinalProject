namespace SchedulingGenerate.Services.Messages;

public class SchedulingCrudStudentCourseRequestDto
{
    public string MethodType { get; set; }
    public string CourseId { get; set; }
    public string StudentId { get; set; }
    public string oldCourseId { get; set; }
    public string oldStudentId { get; set; }
}