using System.ComponentModel.DataAnnotations;

namespace SchedulingGenerate.Services.Models
{
    public class Student
    {
        [Key] 
        public int Id { get; set; }
        public string StudentId { get; set; }
        public string Name { get; set; }
    }
}