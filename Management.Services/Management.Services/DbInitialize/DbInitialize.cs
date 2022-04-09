using Management.Services.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Management.Services.DbInitialize;


public static class DbInitialize
{
    public static void Initialize(IApplicationBuilder app)
    {
        using (var serviceScope = app.ApplicationServices.CreateScope())
        {
            var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
            context.Database.EnsureCreated();
            
            try
            {
                if (context.Database.GetPendingMigrations().Count() > 0)
                {
                    context.Database.Migrate();
                    Console.WriteLine("====> migrations");
                }
            }
            catch(Exception ex)
            {
            
            }
            
        }
    }
}
