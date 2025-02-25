using Courses.DAL.Model;

namespace Courses.DAL.Data
{
    public interface ITeacherDataProvider
    {
        Task<IEnumerable<Teacher>?> GetAllAsync();
    }
}
