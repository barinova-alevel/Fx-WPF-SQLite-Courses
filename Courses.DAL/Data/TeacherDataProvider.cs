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

        public async Task UpdateAsync(Teacher teacher)
        {
            var trackedTeacher = await _context.Teachers.FindAsync(teacher.TeacherId);
            if (trackedTeacher != null)
            {
                trackedTeacher.FirstName = teacher.FirstName;
                trackedTeacher.LastName = teacher.LastName;
                await _context.SaveChangesAsync();
            }
        }

        public async Task AddAsync(Teacher teacher)
        {
            _context.Teachers.Add(teacher);
            await _context.SaveChangesAsync();
        }
    }
}
