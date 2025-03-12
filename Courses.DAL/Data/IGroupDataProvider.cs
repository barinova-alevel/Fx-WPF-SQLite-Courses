using Courses.DAL.Models;

namespace Courses.DAL.Data
{
    public interface IGroupDataProvider
    {
        Task<IEnumerable<StudentsGroup>?> GetAllAsync();

        Task UpdateAsync(StudentsGroup group);
    }
}
