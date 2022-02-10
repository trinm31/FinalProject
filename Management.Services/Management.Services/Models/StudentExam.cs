using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Management.Services.Models;

public class StudentExam
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string StudentId { get; set; }
    [Required]
    public string ExamId { get; set; }
}