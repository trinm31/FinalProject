namespace Management.Services.Dtos;

public class ExamDto
{
    public int Id { get; set; }
    public string ExamId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool Status { get; set; }
}