using Courses.DAL.Models;

namespace Courses.WPF.ViewModel
{
    public class TeacherItemViewModel : ViewModelBase
    {
        private readonly Teacher _model;

        public Teacher Model => _model; // Model for database updates
        public TeacherItemViewModel(Teacher model)
        {
            _model = model;
        }
        public int TeacherId => _model.TeacherId;

        public string? FirstName
        {
            get => _model.FirstName;
            set
            {
                _model.FirstName = value;
                RaisePropertyChanged();
            }
        }

        public string? LastName
        {
            get => _model.LastName;
            set
            {
                _model.LastName = value;
                RaisePropertyChanged();
            }
        }
        public override bool Equals(object? obj)
        {
            if (obj is TeacherItemViewModel other)
            {
                return this.TeacherId == other.TeacherId;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return TeacherId.GetHashCode();
        }

    }
}
