using System.Windows;
using Courses.DAL.Data;
using Courses.WPF.ViewModel;
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
            //services.AddDbContext<AppDbContext>(options =>
               //options.UseSqlite("Data Source=courseswpf.db"));
               //options.UseSqlite("Data Source=C:\\Users\\Oksana\\courseswpf.db"));
             
            //services.AddScoped<MainWindow>();
            services.AddSingleton<MainWindow>();

            services.AddScoped<MainViewModel>();
            services.AddScoped<CoursesViewModel>();
            services.AddScoped<GroupsViewModel>();

            services.AddScoped<ICourseDataProvider, CourseDataProvider>();
            services.AddScoped<IGroupDataProvider, GroupDataProvider>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var mainWindow = _serviceProvider.GetService<MainWindow>();
            //temporary:
            if (mainWindow == null)
            {
                MessageBox.Show("Failed to resolve MainWindow. Check your service registrations.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            mainWindow?.Show();
        }
    }
}
