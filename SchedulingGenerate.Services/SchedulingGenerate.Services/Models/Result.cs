using System;
using System.ComponentModel.DataAnnotations;

namespace SchedulingGenerate.Services.Models
{
    public class Result
    {
        [Key]
        public int Id { get; set; }
        public string CourseId { get; set; }
        public int Color { get; set; }
        public DateTime Date { get; set; }
        public int Slot { get; set; }
    }
}