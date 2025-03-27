using System.Windows;
using System.Windows.Controls;
using Courses.DAL.Data;
using Courses.DAL.Models;
using Courses.WPF.ViewModel;

namespace Courses.WPF.View
{
    public partial class CoursesView : UserControl
    {
        private CoursesViewModel _viewModel;
        public CoursesView(CoursesViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = _viewModel;
            Loaded += CoursesView_Loaded;
        }

        public CoursesView()
        {
            InitializeComponent();
        }

        private async void CoursesView_Loaded(object sender, RoutedEventArgs e)
        {
            await _viewModel.LoadAsync();
        }
        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (e.NewValue is StudentsGroup selectedGroup)
            {
                Console.WriteLine($"Group selected: {selectedGroup.Name}");
                var convertedGroup = MapStudentsGroupToGroupItemViewModel(selectedGroup);

                var viewModel = DataContext as CoursesViewModel;
                if (viewModel != null && selectedGroup !=null)
                {
                    //viewModel.SelectedGroup = selectedGroup;
                    viewModel.SelectedGroup = convertedGroup;
                }
            }
        }

        private GroupItemViewModel? MapStudentsGroupToGroupItemViewModel(StudentsGroup? studentsGroup)
        {
            if (studentsGroup == null)
            {
                return null;
            }

            return new GroupItemViewModel(studentsGroup);
        }
    }
}
