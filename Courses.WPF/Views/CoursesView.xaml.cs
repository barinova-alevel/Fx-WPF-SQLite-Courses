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
    }
}
