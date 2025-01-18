using Courses.WPF.Model;

namespace Courses.WPF.Data
{
    public interface ICourseDataProvider
    {
        Task<IEnumerable<Course>?> GetAllAsync();
    }
}
