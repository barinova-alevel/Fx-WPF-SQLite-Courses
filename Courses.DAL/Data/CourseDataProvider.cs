using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Courses.DAL;
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
            //SQLite Exception here: no such table 'Courses'
            return await _context.Courses.ToListAsync();

            //using (var context = new AppDbContext())
            //{
            //    return context.Courses.ToList();
            //}
        }
    }
}
