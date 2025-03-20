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
            }
        }

        //add SelectedGroupStudents
       // public ObservableCollection<Student> SelectedGroupStudents =>
         //  new ObservableCollection<Student>(_selectedGroup?.Students ?? Enumerable.Empty<Student>());

        public GroupItemViewModel? SelectedGroup
        {
            get => _selectedGroup;
            set
            {
                _selectedGroup = value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(IsGroupSelected));
               
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

        //private async void LoadCoursesAsync()
        //{
        //    var courses = await _courseDataProvider.GetAllAsync();
        //    if (courses != null)
        //    {
        //        Courses.Clear();
        //        foreach (var course in courses)
        //        {
        //            Courses.Add(new CourseItemViewModel(course));
        //        }
        //    }
        //}

        //public async Task LoadCoursesWithGroups()
        //{
        //    var courses = await _courseDataProvider.GetAllAsync();
        //    var courses = context.Courses.Include(c => c.Groups).ToList();
        //    Courses = new ObservableCollection<Course>(courses);

        //}
    }
}
