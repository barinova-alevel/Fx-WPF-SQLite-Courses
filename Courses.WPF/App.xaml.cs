using System.Windows;
using Courses.WPF.Data;
using Courses.WPF.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Courses.WPF
{
    public partial class App : Application
    {
        private readonly ServiceProvider _serviceProvider;

        public App()
        {
            ServiceCollection services = new ServiceCollection();
            ConfigureServices(services);
            _serviceProvider = services.BuildServiceProvider();
        }

        private void ConfigureServices(ServiceCollection services)
        {
            services.AddTransient<MainWindow>();

            services.AddTransient<MainViewModel>();
            services.AddTransient<CoursesViewModel>();
            services.AddTransient<GroupsViewModel>();

            services.AddTransient<ICourseDataProvider, CourseDataProvider>();
            services.AddTransient<IGroupDataProvider, GroupDataProvider>();

            //using (AppDbContext context = new AppDbContext())
            //{
            //    services.AddDbContext<>();
            //}
                
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var mainVindow = _serviceProvider.GetService<MainWindow>();
            mainVindow?.Show();
        }
    }

}
