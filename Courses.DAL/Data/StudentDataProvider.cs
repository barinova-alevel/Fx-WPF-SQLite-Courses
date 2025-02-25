using Courses.DAL.Model;
using Microsoft.EntityFrameworkCore;

namespace Courses.DAL.Data
{
    public class StudentDataProvider : IStudentDataProvider
    {
        private readonly AppDbContext _context;

        public StudentDataProvider(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Student>?> GetAllAsync()
        {
            return await _context.Students.ToListAsync();
        }
    }
}
