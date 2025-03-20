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

        //got exception if use following:
        //public ObservableCollection<StudentsGroup> StudentsGroups
        //{
        //    get => (ObservableCollection<StudentsGroup>)_model.StudentsGroups;
        //    set
        //    {
        //        _model.StudentsGroups = value;
        //        RaisePropertyChanged();
        //    }
        //}

        //public ICollection<Student> Students
        //{
        //    get => _model.Students;
        //    set
        //    {
        //        _model.Students = value;
        //        RaisePropertyChanged();
        //    }
        //}
    }
}