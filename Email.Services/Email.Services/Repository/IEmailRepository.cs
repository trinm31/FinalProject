using Email.Services.Messages;

namespace Email.Services.Repository;

public interface IEmailRepository
{
    Task SendAndLogEmail(EmailMessage message);
}