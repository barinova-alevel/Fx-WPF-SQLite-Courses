using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using Courses.DAL.Models;

namespace Courses.WPF.ViewModel
{
    public class GroupItemViewModel : ViewModelBase
    {
        private readonly StudentsGroup _model;
        public StudentsGroup Model => _model;
        public GroupItemViewModel(StudentsGroup model)
        {
            _model = model;
            Students = new ObservableCollection<StudentItemViewModel>(
            model.Students?.Select(s => new StudentItemViewModel(s)) ?? Enumerable.Empty<StudentItemViewModel>()
        );
        }
        public int Id => _model.Id;
        public string? Name
        {
            get => _model.Name;
            set
            {
                if (_model.Name != value)
                {
                    _model.Name = value;
                    RaisePropertyChanged();
                }
            }
        }
        public int CourseId
        {
            get => _model.CourseId;
            set
            {
                _model.CourseId = value;
                RaisePropertyChanged();
            }
        }

        public Teacher? Teacher
        {
            get => _model.Teacher;
            set
            {
                _model.Teacher = value;
                RaisePropertyChanged();
            }
        }

        public int? TeacherId
        {
            get => _model.TeacherId;
            set
            {
                if (_model.TeacherId != value)
                {
                    _model.TeacherId = value;
                    RaisePropertyChanged();
                }
            }
        }
        public ObservableCollection<StudentItemViewModel>? Students { get; }

        public int StudentCount => Students?.Count ?? 0;

        public void UpdateStudentCount()
        {
            RaisePropertyChanged(nameof(StudentCount));
        }
    }
}
