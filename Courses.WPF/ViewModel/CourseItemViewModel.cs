using Courses.WPF.Model;

namespace Courses.WPF.ViewModel
{
    public class CourseItemViewModel: ViewModelBase
    {
        private readonly Course _model;

        public CourseItemViewModel(Course model)
        {
            _model = model;
        }
        public int CourseId => _model.CourseId;

        public string? FirstName
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
    }
}