using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Management.Services.Models;

public class StudentExam
{
    [Key]
    public int Id { get; set; }
    [Required]
    public int StudentId { get; set; }
    [ForeignKey("StudentId")]
    public virtual Student Student { get; set; }
    [Required]
    public int ExamId { get; set; }
    [ForeignKey("ExamId")]
    public virtual Exam Exam { get; set; }
    public DateTime TimeStamp { get; set; }
}