using System.ComponentModel.DataAnnotations;

namespace SchedulingGenerate.Services.Models
{
    public class Course
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
    }
}