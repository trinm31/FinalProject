using Management.Services.Models;

namespace Management.Services.Dtos;

public class StudentExamReportDto
{
    public ExamDto Exam { get; set; }
    public IEnumerable<StudentExamDto> StudentExams { get; set; }
}