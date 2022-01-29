using System.ComponentModel.DataAnnotations;

namespace Management.Services.Models;

public class Exam
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }

    public string Description { get; set; }

    public bool Status { get; set; }
}