using System.Collections.ObjectModel;
using Courses.DAL.Data;
using Courses.DAL.Models;
using Courses.WPF.Command;

namespace Courses.WPF.ViewModel
{
    public class GroupsViewModel : ViewModelBase
    {
        private readonly IGroupDataProvider _groupDataProvider;
        private GroupItemViewModel? _selectedGroup;
        private TeacherItemViewModel? _selectedTeacher;

        public GroupsViewModel(IGroupDataProvider groupDataProvider)
        {
            _groupDataProvider = groupDataProvider;
            ImportCommand = new DelegateCommand(Import);
            CreateCommand = new DelegateCommand(Create);
            DeleteCommand = new DelegateCommand(Delete, CanDelete);
        }

        public ObservableCollection<GroupItemViewModel> Groups { get; } = new();
        public ObservableCollection<TeacherItemViewModel> Teachers { get; } = new();
        public GroupItemViewModel? SelectedGroup
        {
            get => _selectedGroup;
            set
            {
                _selectedGroup = value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(IsGroupSelected));
                DeleteCommand.RaiseCanExecuteChanged();
            }
        }

        public TeacherItemViewModel? SelectedTeacher
        {
            get => _selectedTeacher;
            set
            {
                _selectedTeacher = value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(IsGroupSelected));
            }
        }

        public bool IsGroupSelected => SelectedGroup is not null;

        public DelegateCommand ImportCommand { get; }
        public DelegateCommand CreateCommand { get; }
        public DelegateCommand DeleteCommand { get; }

        public async override Task LoadAsync()
        {
            if (Groups.Any())
            {
                return;
            }

            var groups = await _groupDataProvider.GetAllAsync();

            if (groups is not null)
            {
                foreach (var group in groups)
                {
                    Groups.Add(new GroupItemViewModel(group));
                }
            }
        }

        public void Import(object? parameter)
        {
            throw new NotImplementedException();
        }

        private void Create(object? parameter)
        {
            var group = new StudentsGroup { Name = "Group name" };
            var viewModel = new GroupItemViewModel(group);
            Groups.Add(viewModel);
            SelectedGroup = viewModel;
        }

        private void Delete(object? parameter)
        {
            if (SelectedGroup is not null)
            {
                Groups.Remove(SelectedGroup);
                SelectedGroup = null;
            }
        }

        private bool CanDelete(object? parameter) => SelectedGroup is not null;
    }
}
