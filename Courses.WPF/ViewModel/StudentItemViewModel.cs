using Courses.DAL.Model;

namespace Courses.WPF.ViewModel
{
    public class StudentItemViewModel: ViewModelBase
    {
        private readonly Student _model;

        public StudentItemViewModel(Student model)
        {
            _model = model;
        }
        public int StudentId => _model.StudentId;

        public int GroupId
        {
            get => _model.GroupId;
            set
            {
                _model.GroupId = value;
                RaisePropertyChanged();
            }
        }

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
    }
}
