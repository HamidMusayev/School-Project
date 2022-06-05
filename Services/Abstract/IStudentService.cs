using SchoolProject.Models.Classes;

namespace SchoolProject.Services.Abstract
{
    public interface IStudentService
    {
        public Task<List<User>> GetAllAsync();
        public Task<User?> GetByIdAsync(int id);
        public Task AddAsync(User user);
        public Task UpdateAsync(User user);
        public Task DeleteAsync(User user);
    }
}
