using Courses.DAL.Models;

namespace Courses.DAL.Data
{
    public interface IStudentDataProvider
    {
        Task<IEnumerable<Student>?> GetAllAsync();
    }
}
