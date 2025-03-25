using Courses.DAL.Data;
using Courses.DAL.Models;
using Courses.WPF.Command;
using System.Collections.ObjectModel;
using System.Diagnostics;

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
            SaveCommand = new DelegateCommand(async (param) => await SaveAsync(param));
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
        public DelegateCommand SaveCommand { get; }

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

        private async Task SaveAsync(object? parameter)
        {
            if (SelectedStudent == null)
                return;

            try
            {
                var student = MapToStudent(SelectedStudent);

                if (student != null && student.StudentId == 0)
                {
                    await _studentDataProvider.AddAsync(student);
                }
                else
                {
                    await _studentDataProvider.UpdateAsync(student);
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error saving student: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Debug.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }
            }
        }

        private Student MapToStudent(StudentItemViewModel studentVm)
        {
            return new Student
            {
                StudentId = studentVm.StudentId,
                FirstName = studentVm.FirstName,
                LastName = studentVm.LastName,
                GroupId = studentVm.GroupId
            };
        }
    }
}
