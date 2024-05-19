namespace CIoTDSystem.Services
{
    public interface IUserService
    {
        Task<List<User>> GetAllUsersAsync();
        Task<User?> GetSingleUserAsync(int id);
        Task<User> CreateUserAsync(User user);
        Task<User?> UpdateUserAsync(int id, User request);
        Task<User?> DeleteUserAsync(int id);
        Task<User?> ChangeStatusAsync(int id);
    }
}
