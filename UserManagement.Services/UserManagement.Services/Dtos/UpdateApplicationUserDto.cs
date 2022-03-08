using System.ComponentModel.DataAnnotations;

namespace UserManagement.Services.Dtos;

public class UpdateApplicationUserDto
{
    [Required]
    public string Id { get; set; }
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    public string Position { get; set; }
    [Required]
    public string PersionalId { get; set; }
    [Required]
    public string PhoneNumber { get; set; }
}