using Courses.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Courses.DAL.Data
{
    public class CourseDataProvider : ICourseDataProvider
    {
        private readonly CourseswpfContext _context;

        public CourseDataProvider(CourseswpfContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Course>?> GetAllAsync()
        {
            return await _context.Courses
                .Include(c => c.StudentsGroups)
                    .ThenInclude(sg => sg.Students)
                .Select(c => new Course
                {
                    CourseId = c.CourseId,
                    Name = c.Name,
                    Description = c.Description,
                    StudentsGroups = c.StudentsGroups != null
                        ? c.StudentsGroups.Select(sg => new StudentsGroup
                        {
                            Id = sg.Id,
                            Name = sg.Name,
                            CourseId = sg.CourseId,
                            TeacherId = sg.TeacherId,
                            Students = sg.Students != null
                                ? new List<Student>(sg.Students)
                                : new List<Student>()
                        }).ToList()
                        : new List<StudentsGroup>()
                })
                .ToListAsync();
        }
        //
        //public async Task<IEnumerable<Course>?> GetAllAsync()
        //{
        //    return await _context.Courses
        //        .Include(c => c.StudentsGroups)
        //        .Select(c => new Course
        //        {
        //            CourseId = c.CourseId,
        //            Name = c.Name,
        //            Description = c.Description,
        //            StudentsGroups = c.StudentsGroups != null
        //                ? new List<StudentsGroup>(c.StudentsGroups)
        //                : new List<StudentsGroup>()
        //        })
        //        .ToListAsync();
        //}

    }
}
