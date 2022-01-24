namespace Email.Services.Messages;

public class EmailMessage
{
    public int Type { get; set; }
    public bool Status { get; set; }
    public string Email { get; set; }
    public string FileName { get; set; }
    public Byte[] FileData { get; set; }
}