
using System.ComponentModel.DataAnnotations;

namespace Management.Services.Models;

public class Schedule
{
    [Key]
    public int Id { get; set; }
    public string CourseId { get; set; }
    public int Color { get; set; }
    public DateTime Date { get; set; }
    public int Slot { get; set; }
}