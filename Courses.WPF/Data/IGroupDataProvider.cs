using Courses.WPF.Model;

namespace Courses.WPF.Data
{
    public interface IGroupDataProvider
    {
        Task<IEnumerable<Group>?> GetAllAsync();
    }
}
