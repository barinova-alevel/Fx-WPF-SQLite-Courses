namespace Courses.WPF.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly CoursesViewModel _coursesViewModel;
        private ViewModelBase? _selectedViewModel;
        public MainViewModel(CoursesViewModel coursesViewModel)
        {
            _coursesViewModel = coursesViewModel;
            SelectedViewModel = _coursesViewModel;
        }
        public ViewModelBase? SelectedViewModel
		{
			get => _selectedViewModel;
			set 
			{ _selectedViewModel = value;
				RaisePropertyChanged();
			}
		}
        public async override Task LoadAsync() 
        { 
            if (SelectedViewModel is not null) 
            {
                await SelectedViewModel.LoadAsync();
            }
        }
	}
}
