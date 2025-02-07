using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Courses.WPF.Model;
using Microsoft.EntityFrameworkCore;

namespace Courses.WPF.Data
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
            //return _context.Courses.ToList();
            return await _context.Courses.ToListAsync();

            //using (var context = new AppDbContext())
            //{
            //    return context.Courses.ToList();
            //}

            ////access db here
            //await Task.Delay(100); //to be deleted
            //return new List<Course>
            //{
            //    new Course{CourseId = 1, Name="11name", Description="111" }
            //};
        }
    }
}
