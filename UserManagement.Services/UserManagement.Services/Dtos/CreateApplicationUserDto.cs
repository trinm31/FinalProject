using System.ComponentModel.DataAnnotations;

namespace UserManagement.Services.Dtos;

public class CreateApplicationUserDto
{
    [Required]
    public string Username { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    [Required]
    [DataType(DataType.Password)]
    [Compare("Password")]
    public string ConfirmPassword { get; set; }
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    public string Position { get; set; }
    [Required]
    public string PhoneNumber { get; set; }
    [Required]
    public string PersionalId { get; set; }
    [Required]
    public string RoleName { get; set; }
}