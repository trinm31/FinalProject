using System.ComponentModel.DataAnnotations;

namespace Management.Services.Models;

public class Student
{
    [Key]
    public int Id { get; set; }
    public string StudentId { get; set; }
    public string Name { get; set; }
    public byte[] Avatar { get; set; }
    public string Email { get; set; }
    private DateTime CreateAt { get; }
    public DateTime? UpdateAt { get; set; }
    public byte[] Qr { get; set; }
    
    public Student()
    {
        this.CreateAt = DateTime.Now;
    }
}