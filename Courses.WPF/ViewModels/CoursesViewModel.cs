using System.Collections.ObjectModel;
using Courses.DAL.Data;
using Courses.DAL.Models;

namespace Courses.WPF.ViewModel
{
    public class CoursesViewModel : ViewModelBase
    {
        private readonly ICourseDataProvider _courseDataProvider;
        private CourseItemViewModel? _selectedCourse;
        private GroupItemViewModel? _selectedGroup;

        public CoursesViewModel(ICourseDataProvider courseDataProvider)
        {
            _courseDataProvider = courseDataProvider;
            LoadAsync();
        }
        public ObservableCollection<CourseItemViewModel> Courses { get; } = new();
        public ObservableCollection<GroupItemViewModel> Groups { get; } = new();
        public ObservableCollection<StudentItemViewModel>? Students { get; } = new();
        public CourseItemViewModel? SelectedCourse
        {
            get => _selectedCourse;
            set
            {
                _selectedCourse = value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(IsCourseSelected));
                //LoadGroups();
            }
        }

        public GroupItemViewModel? SelectedGroup
        {
            get => _selectedGroup;
            set
            {
                _selectedGroup = value;
                Console.WriteLine($"SelectedGroup changed to: {_selectedGroup.Name}");
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(SelectedGroup));
                RaisePropertyChanged(nameof(SelectedGroup.Students));
                RaisePropertyChanged(nameof(IsGroupSelected));
                LoadStudents();
            }
        }

        public bool IsCourseSelected => SelectedCourse is not null;
        public bool IsGroupSelected => SelectedGroup is not null;

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

        private void LoadGroups()
        {
            Groups.Clear();
            if (SelectedCourse != null && SelectedCourse.StudentsGroups != null)
            {
                foreach (var group in SelectedCourse.StudentsGroups)
                {
                    Groups.Add(new GroupItemViewModel(group));
                }
            }
        }

        private void LoadStudents()
        {
            if (Students is not null)
            {
                Students.Clear();
            }
            if (SelectedGroup != null && SelectedGroup.Students != null)
            {
                foreach (var student in SelectedGroup.Students)
                {
                    Students.Add(student);
                }
            }
        }

    }
}
