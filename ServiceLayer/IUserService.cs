using EntityLayer;

namespace ServiceLayer
{
    public interface IUserService
    {
        Task<List<User>> GetUsersAsync();
        Task<User> GetUserAsync(int id);
        Task AddUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(int id);
        Task<bool> UserExistsAsync(int id);
    }
}
