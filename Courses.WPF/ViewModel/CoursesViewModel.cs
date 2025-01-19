using System.Collections.ObjectModel;
using Courses.WPF.Data;
using Courses.WPF.Model;

namespace Courses.WPF.ViewModel
{
    public class CoursesViewModel
    {
        private readonly ICourseDataProvider _courseDataProvider;
        public CoursesViewModel(ICourseDataProvider courseDataProvider)
        {
            _courseDataProvider = courseDataProvider;
        }
        public ObservableCollection<Course> Courses { get; } = new();
        public Course? SelectedCourse { get; set; }
        public async Task LoadAsync()
        {
            if (Courses.Any())
            {
                return;
            }

            var courses = await _courseDataProvider.GetAllAsync();

            if (courses is not null)
            {
                foreach (var course in courses)
                {
                    Courses.Add(course);   
                }
            }
        }
    }
}
