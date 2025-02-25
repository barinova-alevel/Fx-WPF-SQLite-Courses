using Courses.DAL.Data;
using System.Collections.ObjectModel;

namespace Courses.WPF.ViewModel
{
    public class TeachersViewModel : ViewModelBase
    {
        private readonly ITeacherDataProvider _teacherDataProvider;
        private TeacherItemViewModel? _selectedTeacher;
        public TeachersViewModel(ITeacherDataProvider teacherDataProvider)
        {
            _teacherDataProvider = teacherDataProvider;
        }
        public ObservableCollection<TeacherItemViewModel> Teachers { get; } = new();
        public TeacherItemViewModel? SelectedTeacher
        {
            get => _selectedTeacher;
            set
            {
                _selectedTeacher = value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(IsTeacherSelected));
            }
        }
        public bool IsTeacherSelected => SelectedTeacher is not null;

        public override async Task LoadAsync()
        {
            if (Teachers.Any())
            {
                return;
            }

            var teachers = await _teacherDataProvider.GetAllAsync();

            if (teachers is not null)
            {
                foreach (var teacher in teachers)
                {
                    Teachers.Add(new TeacherItemViewModel(teacher));
                }
            }
        }
    }
}
