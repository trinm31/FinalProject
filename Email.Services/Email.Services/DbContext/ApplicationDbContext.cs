using Email.Services.Models;
using Microsoft.EntityFrameworkCore;

namespace Email.Services.DbContext;

public class ApplicationDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }
    public DbSet<EmailLog> EmailLogs{ get; set; }

}