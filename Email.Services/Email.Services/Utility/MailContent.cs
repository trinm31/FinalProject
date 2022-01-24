using MimeKit;

namespace Email.Services.Utility;

public class MailContent
{
    public string To { get; set; }              
    public string Subject { get; set; }

    public BodyBuilder BodyBuilder;
}