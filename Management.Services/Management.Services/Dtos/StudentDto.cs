namespace Management.Services.Dtos;

public class StudentDto
{
    public int Id { get; set; }
    public string StudentId { get; set; }
    public string Name { get; set; }
    public byte[] Avatar { get; set; }
    public string Email { get; set; }
    public byte[] Qr { get; set; }
}