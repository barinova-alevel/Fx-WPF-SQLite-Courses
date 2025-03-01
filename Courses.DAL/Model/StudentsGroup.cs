using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace Courses.DAL.Model
{
    public class StudentsGroup
    {
        [Key]
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string? Name { get; set; }
        public Teacher? Teacher { get; set; }
        //public List<Student>? Students { get; set; }

        //[ForeignKey("CourseId")]
        //public Course? Course { get; set; }

    }
}