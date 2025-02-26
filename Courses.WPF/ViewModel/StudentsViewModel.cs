using Courses.DAL.Data;
using Courses.DAL.Model;
using Courses.WPF.Command;
using System.Collections.ObjectModel;

namespace Courses.WPF.ViewModel
{
    public class StudentsViewModel : ViewModelBase
    {
        private readonly IStudentDataProvider _studentDataProvider;
        private StudentItemViewModel? _selectedStudent;
        public StudentsViewModel(IStudentDataProvider studentDataProvider)
        {
            _studentDataProvider = studentDataProvider;
            CreateCommand = new DelegateCommand(Create);
            DeleteCommand = new DelegateCommand(Delete, CanDelete);
        }
        public ObservableCollection<StudentItemViewModel> Students { get; } = new();
        public StudentItemViewModel? SelectedStudent
        {
            get => _selectedStudent;
            set
            {
                _selectedStudent = value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(IsStudentSelected));
                DeleteCommand.RaiseCanExecuteChanged();
            }
        }
        public bool IsStudentSelected => SelectedStudent is not null;

        public DelegateCommand CreateCommand { get; }
        public DelegateCommand DeleteCommand { get; }

        public override async Task LoadAsync()
        {
            if (Students.Any())
            {
                return;
            }

            var students = await _studentDataProvider.GetAllAsync();

            if (students is not null)
            {
                foreach (var student in students)
                {
                    Students.Add(new StudentItemViewModel(student));
                }
            }
        }
        private void Create(object? parameter)
        {
            var student = new Student { FirstName = "Name" };
            var viewModel = new StudentItemViewModel(student);
            Students.Add(viewModel);
            SelectedStudent = viewModel;
        }

        private void Delete(object? parameter)
        {
            if (SelectedStudent is not null)
            {
                Students.Remove(SelectedStudent);
                SelectedStudent = null;
            }
        }

        private bool CanDelete(object? parameter) => SelectedStudent is not null;
    }
}
