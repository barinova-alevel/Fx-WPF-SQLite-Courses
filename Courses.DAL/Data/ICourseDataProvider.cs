using Courses.DAL.Model;

namespace Courses.DAL.Data
{
    public interface ICourseDataProvider
    {
        Task<IEnumerable<Course>?> GetAllAsync();
    }
}
