using Microsoft.EntityFrameworkCore;

namespace Management.Services.DbContext;

public class ApplicationDbContext: Microsoft.EntityFrameworkCore.DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }
}