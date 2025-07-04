﻿using Courses.WPF.Command;

namespace Courses.WPF.ViewModel
{
    public class MainViewModel : ViewModelBase
    {

        private ViewModelBase? _selectedViewModel;
        public MainViewModel(
              CoursesViewModel coursesViewModel,
            GroupsViewModel groupsViewModel,
            StudentsViewModel studentsViewModel,
            TeachersViewModel teachersViewModel)

        {
            CoursesViewModel = coursesViewModel;
            GroupsViewModel = groupsViewModel;
            StudentsViewModel = studentsViewModel;
            TeachersViewModel = teachersViewModel;
            SelectedViewModel = CoursesViewModel;
            SelectViewModelCommand = new DelegateCommand(SelectViewModel);
        }

        public ViewModelBase? SelectedViewModel
        {
            get => _selectedViewModel;
            set
            {
                _selectedViewModel = value;
                RaisePropertyChanged();
            }
        }
        public CoursesViewModel CoursesViewModel { get; }
        public GroupsViewModel GroupsViewModel { get; }
        public StudentsViewModel StudentsViewModel { get; }
        public TeachersViewModel TeachersViewModel { get; }
        public DelegateCommand SelectViewModelCommand { get; }
        public async override Task LoadAsync()
        {
            if (SelectedViewModel is not null)
            {
                await SelectedViewModel.LoadAsync();
            }
        }

        private async void SelectViewModel(object? parameter)
        {
            SelectedViewModel = parameter as ViewModelBase;
            await LoadAsync();
        }
    }
}
