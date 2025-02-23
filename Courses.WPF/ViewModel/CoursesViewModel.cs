using System.Collections.ObjectModel;
using Courses.DAL.Data;

namespace Courses.WPF.ViewModel
{
    public class CoursesViewModel :ViewModelBase
    {
        private readonly ICourseDataProvider _courseDataProvider;
        private CourseItemViewModel? _selectedCourse;
        public CoursesViewModel(ICourseDataProvider courseDataProvider)
        {
            _courseDataProvider = courseDataProvider;
        }
        public ObservableCollection<CourseItemViewModel> Courses { get; } = new();
        public CourseItemViewModel? SelectedCourse
        {
            get => _selectedCourse;
            set
            {
                _selectedCourse = value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(IsCourseSelected));
            }
        }
        public bool IsCourseSelected => SelectedCourse is not null;

        public override async Task LoadAsync()
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
                    Courses.Add(new CourseItemViewModel(course));   
                }
            }
        }
    }
}
