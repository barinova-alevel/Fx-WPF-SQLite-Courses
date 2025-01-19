using Courses.WPF.Model;

namespace Courses.WPF.Data
{
    public class GroupDataProvider : IGroupDataProvider
    {
        public async Task<IEnumerable<Group>?> GetAllAsync()
        {
            //access db here
            await Task.Delay(100); //to be deleted
            return new List<Group>
            {
                new Group{GroupId=1, CourseId = 1, Name="Group 1" }
            };
        }
    }
}
