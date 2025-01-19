using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Courses.WPF.Data;
using Courses.WPF.Model;

namespace Courses.WPF.ViewModel
{
    public class GroupsViewModel : INotifyPropertyChanged
    {
        private readonly IGroupDataProvider _groupDataProvider;
        private Group? _selectedGroup;

        public event PropertyChangedEventHandler? PropertyChanged;

        public GroupsViewModel(IGroupDataProvider groupDataProvider)
        {
            _groupDataProvider = groupDataProvider;
        }
        public ObservableCollection<Group> Groups { get; } = new();
        public Group? SelectedGroup { 
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
                    Groups.Add(group);
                }
            }
        }

        public void Add()
        {
            throw new NotImplementedException();
        }

        internal void Create()
        {
            var group = new Group { Name = "Group name"};
            Groups.Add(group);
            SelectedGroup = group;
        }

        private void RaisePropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
