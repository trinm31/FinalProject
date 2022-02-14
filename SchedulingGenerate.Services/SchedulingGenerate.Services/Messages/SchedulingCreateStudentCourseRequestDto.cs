using SchedulingGenerate.Services.Models;

namespace SchedulingGenerate.Services.Messages;

public class SchedulingCreateStudentCourseRequestDto
{
    public List<StudentCourse> StudentCourses { get; set; }
}