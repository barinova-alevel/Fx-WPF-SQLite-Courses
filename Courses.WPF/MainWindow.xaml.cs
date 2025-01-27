using System.Windows;
using Courses.WPF.Data;
using Courses.WPF.ViewModel;

namespace Courses.WPF
{
    public partial class MainWindow : Window
    {
        private readonly MainViewModel _viewModel;
        public MainWindow()
        {
            InitializeComponent();
            _viewModel = new MainViewModel(new CoursesViewModel(new CourseDataProvider()));
           //_viewModel = new MainViewModel(new GroupsViewModel(new GroupDataProvider()));
            DataContext = _viewModel;
            Loaded += MainWindow_Loaded;
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await _viewModel.LoadAsync();
        }
    }
}