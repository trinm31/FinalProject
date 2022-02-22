using System.ComponentModel.DataAnnotations;

namespace Management.Services.Models;

public class Checkin
{
    [Key]
    public int Id { get; set; }
    [Required]
    public int RoomId { get; set; }
    [Required]
    public string StudentId { get; set; }
}