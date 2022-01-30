namespace Management.Services.Dtos;

public class CheckQrDto
{
    public string StudentId { get; set; }
    public string Name { get; set; }
    public byte[] Qr { get; set; }
}