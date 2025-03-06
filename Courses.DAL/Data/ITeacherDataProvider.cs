using Courses.DAL.Models;

namespace Courses.DAL.Data
{
    public interface ITeacherDataProvider
    {
        Task<IEnumerable<Teacher>?> GetAllAsync();
    }
}
