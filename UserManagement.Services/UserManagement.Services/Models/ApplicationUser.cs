using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace UserManagement.Services.Models;

public class ApplicationUser: IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Position { get; set; }
    public string PersionalId { get; set; }
    [NotMapped] 
    public string Role { get; set; }
}