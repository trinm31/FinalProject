using Email.Services.DbContext;
using Email.Services.Messages;
using Email.Services.Models;
using Microsoft.EntityFrameworkCore;

namespace Email.Services.Repository;

public class EmailRepository : IEmailRepository
{
    private readonly DbContextOptions<ApplicationDbContext> _dbContext;

    public EmailRepository(DbContextOptions<ApplicationDbContext> dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task SendAndLogEmail(EmailMessage message)
    {
        //implement an email sender or call some other class library
        EmailLog emailLog = new EmailLog()
        {
            Email = message.Email,
            EmailSent = DateTime.Now,
            Log = $"Email - {message.Type} with file name { message.FileName} has been created successfully."
        };

        await using var _db = new ApplicationDbContext(_dbContext);
        _db.EmailLogs.Add(emailLog);
        await _db.SaveChangesAsync();
    }
}