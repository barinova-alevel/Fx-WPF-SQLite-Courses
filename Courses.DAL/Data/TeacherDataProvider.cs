using Courses.DAL.Model;
using Microsoft.EntityFrameworkCore;

namespace Courses.DAL.Data
{
    public class TeacherDataProvider : ITeacherDataProvider
    {
        private readonly AppDbContext _context;

        public TeacherDataProvider(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Teacher>?> GetAllAsync()
        {
            return await _context.Teachers.ToListAsync();
        }
    }
}
