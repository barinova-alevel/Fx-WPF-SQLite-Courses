using System.ComponentModel.DataAnnotations;

namespace Courses.WPF.Model
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}