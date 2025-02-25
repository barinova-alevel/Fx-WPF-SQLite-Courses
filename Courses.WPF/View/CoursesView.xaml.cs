using System.Windows;
using System.Windows.Controls;
using Courses.WPF.ViewModel;

namespace Courses.WPF.View
{
    public partial class CoursesView : UserControl
    {
        private CoursesViewModel _viewModel;
        //AppDbContext context = new AppDbContext();
        public CoursesView(CoursesViewModel viewModel)
        {
            InitializeComponent();
            //_viewModel = new CoursesViewModel(new CourseDataProvider(context)); //?
            _viewModel = viewModel;
            DataContext = _viewModel;
            Loaded += CoursesView_Loaded;
        }

        //without following constructor got exception in App.mainWindow?.Show();
        public CoursesView()
        {
            InitializeComponent();
            //_viewModel = viewModel;
            //DataContext = _viewModel;
            //Loaded += CoursesView_Loaded;
        }

        private async void CoursesView_Loaded(object sender, RoutedEventArgs e)
        {
            await _viewModel.LoadAsync();
        }
    }
}
