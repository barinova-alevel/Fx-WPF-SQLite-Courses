using System.Collections.ObjectModel;
using Courses.DAL.Models;

namespace Courses.WPF.ViewModel
{
    public class CourseItemViewModel : ViewModelBase
    {
        private readonly Course _model;

        public CourseItemViewModel(Course model)
        {
            _model = model;
        }
        public int CourseId => _model.CourseId;

        public string? Name
        {
            get => _model.Name;
            set
            {
                _model.Name = value;
                RaisePropertyChanged();
            }
        }

        public string? Description
        {
            get => _model.Description;
            set
            {
                _model.Description = value;
                RaisePropertyChanged();
            }
        }

        public ICollection<StudentsGroup> StudentsGroups
        {
            get => _model.StudentsGroups;
            set
            {
                _model.StudentsGroups = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<Student> GetStudentsAsObservable(int groupId)
        {
            return new ObservableCollection<Student>(GetStudentsFromGroup(groupId));
        }

        private ICollection<Student> GetStudentsFromGroup(int groupId)
        {
            var group = _model.StudentsGroups.FirstOrDefault(g => g.Id == groupId);
            return group?.Students ?? new List<Student>();
        }
    }
}