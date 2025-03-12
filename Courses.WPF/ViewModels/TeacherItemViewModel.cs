using Courses.DAL.Models;

namespace Courses.WPF.ViewModel
{
    public class TeacherItemViewModel : ViewModelBase
    {
        private readonly Teacher _model;

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


    }
}
