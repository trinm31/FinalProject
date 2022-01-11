using System.ComponentModel.DataAnnotations;

namespace SchedulingGenerate.Services.Models
{
    public class StudentCourse
    {
        [Key]
        public int Id { get; set; }
        public string CourseId { get; set; }
        public string StudentId { get; set; }
    }
}