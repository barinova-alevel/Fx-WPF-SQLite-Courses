using Courses.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Courses.DAL.Data
{
    public interface IStudentDataProvider
    {
        Task<IEnumerable<Student>?> GetAllAsync();

        Task UpdateAsync(Student student);

        Task AddAsync(Student student);
    }
}
