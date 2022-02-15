using System.ComponentModel.DataAnnotations;

namespace SchedulingGenerate.Services.Models;

public class Setting
{
    [Key]
    public int Id { get; set; }
    [Required]
    public DateTime StartDate { get; set; }
    [Required]
    public DateTime EndDate { get; set; }
    [Required]
    [Range(1, 100)]
    public int ConcurrencyLevelDefault { get; set; }
    [Required]
    [Range(1, 100)]
    public int InternalDistance { get; set; }
    [Required]
    [Range(1, 100)]
    public int ExternalDistance { get; set; }
    [Required]
    [Range(1, 100)]
    public int NoOfTimeSlot { get; set; }
}