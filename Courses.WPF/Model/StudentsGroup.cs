namespace Courses.WPF.Model
{
    public class StudentsGroup
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string? Name { get; set; }
        public Teacher? Teacher { get; set; }

    }
}