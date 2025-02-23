using Courses.DAL.Model;
using Microsoft.EntityFrameworkCore;

namespace Courses.DAL
{
    public class AppDbContext : DbContext
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentsGroup> StudentsGroups { get; set; }
        public DbSet<Teacher> Teachers { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base()
        {

        }

        //the following strings are already used in App.ConfigureServices(), but without those strings got exception ('No database provider has been configured for this DbContext) in CourseDataProvider Task<IEnumerable<Course>?> GetAllAsync() 
        protected override void OnConfiguring(DbContextOptionsBuilder options)
    => options.UseSqlite("Data Source=C:\\Users\\Oksana\\courseswpf.db");
    }
}
