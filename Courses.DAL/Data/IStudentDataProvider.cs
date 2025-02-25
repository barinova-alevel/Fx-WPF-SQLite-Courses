using Courses.DAL.Model;

namespace Courses.DAL.Data
{
    public interface IStudentDataProvider
    {
        Task<IEnumerable<Student>?> GetAllAsync();
    }
}
