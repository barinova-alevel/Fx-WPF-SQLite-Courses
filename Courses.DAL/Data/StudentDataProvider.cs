using Courses.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Courses.DAL.Data
{
    public class StudentDataProvider : IStudentDataProvider
    {
        private readonly CourseswpfContext _context;

        public StudentDataProvider(CourseswpfContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Student>?> GetAllAsync()
        {
            return await _context.Students.ToListAsync();
        }
    }
}
