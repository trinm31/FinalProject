namespace Management.Services.Dtos;

public class ExamCreateDto
{
    public string ExamId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool Status { get; set; }
}