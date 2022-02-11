namespace SchedulingGenerate.Services.Messages;

public class SchedulingCrudStudentCourseRequestDto
{
    public string MethodType { get; set; }
    public int Id { get; set; }
    public string CourseId { get; set; }
    public string StudentId { get; set; }
}