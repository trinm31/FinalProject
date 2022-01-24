namespace Email.Services.Utility;

public interface ISendMailService
{
    Task SendMail(MailContent mailContent);
}