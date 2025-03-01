using Courses.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Courses.DAL.Data
{
    public class TeacherDataProvider : ITeacherDataProvider
    {
        private readonly CourseswpfContext _context;

        public TeacherDataProvider(CourseswpfContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Teacher>?> GetAllAsync()
        {
            return await _context.Teachers.ToListAsync();
        }
    }
}
