using Courses.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Courses.DAL.Data
{
    public class GroupDataProvider : IGroupDataProvider
    {
        private readonly CourseswpfContext _context;

        public GroupDataProvider(CourseswpfContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<StudentsGroup>?> GetAllAsync()
        {
            return await _context.StudentsGroups
                .Include(g => g.Teacher)
                .ToListAsync();
        }
    }
}
