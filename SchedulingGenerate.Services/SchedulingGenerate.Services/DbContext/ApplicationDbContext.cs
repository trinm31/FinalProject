
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SchedulingGenerate.Services.Models;

namespace SchedulingGenerate.Services.DbContext
{
    public class ApplicationDbContext: Microsoft.EntityFrameworkCore.DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Course> Courses { get; set; }  
  
        public DbSet<Student> Students { get; set; }
        
        public DbSet<StudentCourse> StudentCourses { get; set; }
        
        public DbSet<Result> Results { get; set; }
    }
}