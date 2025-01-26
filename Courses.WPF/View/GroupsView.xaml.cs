using System.Windows;
using System.Windows.Controls;
using Courses.WPF.Data;
using Courses.WPF.ViewModel;

namespace Courses.WPF.View
{

    public partial class GroupsView : UserControl
    {
        private GroupsViewModel _viewModel;
        public GroupsView()
        {
            InitializeComponent();
            _viewModel = new GroupsViewModel(new GroupDataProvider());
            DataContext = _viewModel;
            Loaded += GroupsView_Loaded;
        }
        private async void GroupsView_Loaded(object sender, RoutedEventArgs e)
        {
            await _viewModel.LoadAsync();
        }
    }
}
