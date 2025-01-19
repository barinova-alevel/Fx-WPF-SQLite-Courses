using System.Collections.ObjectModel;
using Courses.WPF.Data;
using Courses.WPF.Model;

namespace Courses.WPF.ViewModel
{
    public class GroupsViewModel : ViewModelBase
    {
        private readonly IGroupDataProvider _groupDataProvider;
        private GroupItemViewModel? _selectedGroup;

        public GroupsViewModel(IGroupDataProvider groupDataProvider)
        {
            _groupDataProvider = groupDataProvider;
        }
        public ObservableCollection<GroupItemViewModel> Groups { get; } = new();
        public GroupItemViewModel? SelectedGroup { 
            get => _selectedGroup;
            set 
            { 
                _selectedGroup = value;
                RaisePropertyChanged();
            } 
        }

        public async Task LoadAsync()
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

        public void Add()
        {
            throw new NotImplementedException();
        }

        internal void Create()
        {
            var group = new StudentsGroup { Name = "Group name"};
            var viewModel = new GroupItemViewModel(group);
            Groups.Add(viewModel);
            SelectedGroup = viewModel;
        }

        
    }
}
