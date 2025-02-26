using Courses.DAL.Data;
using Courses.DAL.Model;
using Courses.WPF.Command;
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
            CreateCommand = new DelegateCommand(Create);
            DeleteCommand = new DelegateCommand(Delete, CanDelete);
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
                DeleteCommand.RaiseCanExecuteChanged();
            }
        }
        public bool IsTeacherSelected => SelectedTeacher is not null;

        public DelegateCommand CreateCommand { get; }
        public DelegateCommand DeleteCommand { get; }
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

        private void Create(object? parameter)
        {
            var teacher = new Teacher { FirstName = "Name" };
            var viewModel = new TeacherItemViewModel(teacher);
            Teachers.Add(viewModel);
            SelectedTeacher = viewModel;
        }

        private void Delete(object? parameter)
        {
            if (SelectedTeacher is not null)
            {
                Teachers.Remove(SelectedTeacher);
                SelectedTeacher = null;
            }
        }

        private bool CanDelete(object? parameter) => SelectedTeacher is not null;
    }
}
