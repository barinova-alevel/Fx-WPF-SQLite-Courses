using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Courses.WPF.Model;

namespace Courses.WPF.Data
{
    public class CourseDataProvider : ICourseDataProvider
    {
        public async Task<IEnumerable<Course>?> GetAllAsync()
        { 
            //access db here
            await Task.Delay(100); //to be deleted
            return new List<Course>
            {
                new Course{CourseId = 1, Name="11name", Description="111" }
            };
        }
    }
}
