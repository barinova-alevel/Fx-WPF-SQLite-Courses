using Courses.DAL.Data;
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
            }
        }
        public bool IsStudentSelected => SelectedStudent is not null;

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
    }
}
