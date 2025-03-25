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

        public async Task UpdateAsync(Student student)
        {
            var trackedStudent = await _context.Students.FindAsync(student.StudentId);
            if (trackedStudent != null)
            {
                trackedStudent.FirstName = student.FirstName;
                trackedStudent.LastName = student.LastName;
                trackedStudent.GroupId = student.GroupId;
                await _context.SaveChangesAsync();
            }
        }

        public async Task AddAsync(Student student)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();
        }
    }
}
