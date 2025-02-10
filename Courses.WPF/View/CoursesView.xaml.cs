using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Courses.WPF.Data;
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

        //public CoursesView()
        //{
        //    InitializeComponent();
        //   // _viewModel = viewModel;
        //    DataContext = _viewModel;
        //    Loaded += CoursesView_Loaded;
        //}

        private async void CoursesView_Loaded(object sender, RoutedEventArgs e)
        {
            await _viewModel.LoadAsync();
        }
    }
}
