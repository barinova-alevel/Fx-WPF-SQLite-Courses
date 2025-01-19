namespace Courses.WPF.Model
{
    public class Group
    {
        public int GroupId { get; set; }
        public int CourseId { get; set; }
        public string? Name { get; set; }
        public Teacher? Teacher { get; set; }

    }
}