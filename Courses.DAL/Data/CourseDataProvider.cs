using Courses.DAL.Model;
using Microsoft.EntityFrameworkCore;

namespace Courses.DAL.Data
{
    public class CourseDataProvider : ICourseDataProvider
    {
        private readonly AppDbContext _context;

        public CourseDataProvider(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Course>?> GetAllAsync()
        {
            return await _context.Courses.ToListAsync();
        }
    }
}
