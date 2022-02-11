using System.ComponentModel.DataAnnotations;

namespace SchedulingGenerate.Services.Models
{
    public class Course
    {
        [Key] 
        public int Id { get; set; }
        public string ExamId { get; set; }
        public string Name { get; set; }
    }
}