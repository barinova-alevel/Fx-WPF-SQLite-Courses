using Courses.DAL.Model;

namespace Courses.DAL.Data
{
    public class GroupDataProvider : IGroupDataProvider
    {
        public async Task<IEnumerable<StudentsGroup>?> GetAllAsync()
        {
            //access db here
            await Task.Delay(100); //to be deleted
            return new List<StudentsGroup>
            {
                new StudentsGroup{Id=1, CourseId = 1, Name="Group 1" }
            };
        }
    }
}
