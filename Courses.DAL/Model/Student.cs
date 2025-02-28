using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace Courses.DAL.Model
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }
        public int GroupId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        [ForeignKey("GroupId")]
        public Group? Group { get; set; }
    }
}
