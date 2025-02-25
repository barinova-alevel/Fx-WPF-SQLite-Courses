using Courses.DAL.Model;
using Microsoft.EntityFrameworkCore;

namespace Courses.DAL.Data
{
    public class GroupDataProvider : IGroupDataProvider
    {
        private readonly AppDbContext _context;

        public GroupDataProvider(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<StudentsGroup>?> GetAllAsync()
        {
            return await _context.StudentsGroups.ToListAsync();
        }
    }
}
