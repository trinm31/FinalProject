using System.ComponentModel.DataAnnotations;

namespace Management.Services.Models;

public class Download
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    public string ZipPath { get; set; }
    public DateTime CreateAt { get; set; }
    public DateTime UpdateAt { get; set; }
    public Download()
    {

        CreateAt = DateTime.Now;
    }
}