using ITFundManager.Models;

namespace ITFundManager.Services;

public interface IUserService
{
    Task<User?> AuthenticateAsync(string username, string password);
    Task<int> CreateUserAsync(User user, string password, IEnumerable<string>? roles = null);
    Task<List<User>> GetAllAsync();
    Task<User?> GetByIdAsync(int id);
    Task<int> UpdateAsync(User user);
    Task<int> DeleteAsync(int id);
    Task AssignRolesAsync(int userId, IEnumerable<string> roleNames);
    Task<List<Role>> GetRolesAsync();
    Task<List<Role>> GetRolesForUserAsync(int userId);
}
