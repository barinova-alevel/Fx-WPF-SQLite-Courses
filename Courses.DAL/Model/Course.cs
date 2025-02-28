using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Courses.DAL.Model
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
       public List<StudentsGroup>? StudentsGroups { get; set;}
    }
}