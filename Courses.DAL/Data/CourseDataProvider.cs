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
            //return await _context.Courses.ToListAsync();
            //return await _context.Courses
            //            .Include(c => c.StudentsGroups)
            //            .ToListAsync();
            return await _context.Courses
                         .Select(c => new Course
                         {
                             CourseId = c.CourseId,
                             Name = c.Name,
                             Description = c.Description,
                             StudentsGroups = c.StudentsGroups != null ? new List<StudentsGroup>(c.StudentsGroups) : null
                         })
                         .ToListAsync();
        }
    }
}
