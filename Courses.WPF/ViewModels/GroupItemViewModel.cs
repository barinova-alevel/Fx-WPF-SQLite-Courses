using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using Courses.DAL.Models;

namespace Courses.WPF.ViewModel
{
    public class GroupItemViewModel : ViewModelBase
    {
        private readonly StudentsGroup _model;
        public GroupItemViewModel(StudentsGroup model)
        {
            _model = model;
        }
        public int Id => _model.Id;
        public string? Name
        {
            get => _model.Name;
            set
            {
                _model.Name = value;
                RaisePropertyChanged();
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
        public ObservableCollection<Student>? Students { get; }
    }
}
