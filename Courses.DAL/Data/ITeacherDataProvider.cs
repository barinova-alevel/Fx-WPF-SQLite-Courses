using Courses.DAL.Models;

namespace Courses.DAL.Data
{
    public interface ITeacherDataProvider
    {
        Task<IEnumerable<Teacher>?> GetAllAsync();
        Task UpdateAsync(Teacher teacher);
        Task AddAsync(Teacher teacher);

    }
}
