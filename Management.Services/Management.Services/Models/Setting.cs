using System.ComponentModel.DataAnnotations;

namespace Management.Services.Models;

public class Setting
{
    [Key]
    public int Id { get; set; }
    [Required]
    public DateTime StartDate { get; set; }
    [Required]
    public DateTime EndDate { get; set; }
    [Required]
    [Range(0, 101)]
    public int ConcurrencyLevelDefault { get; set; }
    [Required]
    [Range(0, 101)]
    public int InternalDistance { get; set; }
    [Required]
    [Range(0, 101)]
    public int ExternalDistance { get; set; }
    [Required]
    [Range(0, 101)]
    public int NoOfTimeSlot { get; set; }
}