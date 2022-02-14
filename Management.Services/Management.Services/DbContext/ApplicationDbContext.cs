using Management.Services.Models;
using Microsoft.EntityFrameworkCore;

namespace Management.Services.DbContext;

public class ApplicationDbContext: Microsoft.EntityFrameworkCore.DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Download> Downloads { get; set; }   
    public DbSet<Exam> Exams { get; set; }  
    public DbSet<Student> Students { get; set; }  
    public DbSet<StudentExam> StudentExams { get; set; }
    public DbSet<Schedule> Schedules { get; set; }
    
}