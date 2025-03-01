using System.Windows;
using Courses.DAL;
using Courses.DAL.Data;
using Courses.DAL.Models;
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
            services.AddDbContext<CourseswpfContext>();
            services.AddSingleton<MainWindow>();

            services.AddScoped<MainViewModel>();
            services.AddScoped<CoursesViewModel>();
            services.AddScoped<GroupsViewModel>();
            services.AddScoped<StudentsViewModel>();
            services.AddScoped<TeachersViewModel>();

            services.AddScoped<ICourseDataProvider, CourseDataProvider>();
            services.AddScoped<IGroupDataProvider, GroupDataProvider>();
            services.AddScoped<IStudentDataProvider, StudentDataProvider>();
            services.AddScoped<ITeacherDataProvider, TeacherDataProvider>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var mainWindow = _serviceProvider.GetService<MainWindow>();
            mainWindow?.Show();
        }
    }
}
