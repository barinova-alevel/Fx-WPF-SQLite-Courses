using Courses.DAL.Model;

namespace Courses.DAL.Data
{
    public interface IGroupDataProvider
    {
        Task<IEnumerable<StudentsGroup>?> GetAllAsync();
    }
}
