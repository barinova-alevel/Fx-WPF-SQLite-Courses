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

        public async Task UpdateAsync(StudentsGroup group)
        {
            _context.StudentsGroups.Update(group);
            await _context.SaveChangesAsync();
        }

        public async Task AddAsync(StudentsGroup group)
        {
            _context.StudentsGroups.Add(group);
            await _context.SaveChangesAsync();
        }

        public async Task ReloadAsync(StudentsGroup group)
        {
            await _context.Entry(group).ReloadAsync();
        }

        //public async Task LoadStudentsAsync(StudentsGroup group)
        //{
        //    _context.Entry(group).Collection(g => g.Students).Load();
        //}

        public async Task<StudentsGroup?> GetGroupWithStudentsAsync(int groupId)
        {
            return await _context.StudentsGroups
                .Include(g => g.Students)
                .FirstOrDefaultAsync(g => g.Id == groupId);
        }
    }
}
