using Courses.DAL.Models;

namespace Courses.DAL.Data
{
    public interface IGroupDataProvider
    {
        Task<IEnumerable<StudentsGroup>?> GetAllAsync();

        Task UpdateAsync(StudentsGroup group);

        Task AddAsync(StudentsGroup group);

        Task ReloadAsync(StudentsGroup group);
        //public Task LoadStudentsAsync(StudentsGroup group);
        Task<StudentsGroup?> GetGroupWithStudentsAsync(int groupId);
    }
}
