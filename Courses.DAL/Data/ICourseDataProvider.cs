using Courses.DAL.Models;

namespace Courses.DAL.Data
{
    public interface ICourseDataProvider
    {
        Task<IEnumerable<Course>?> GetAllAsync();
    }
}
