namespace DialedUp.Domain.Users;

public interface IUserRepository
{
    Task<List<User>> GetAllUsersAsync();
    Task<User?> GetUserByIdAsync(int id);
    Task AddUserAsync(User user);
    Task UpdateUserAsync(User user);

    Task<List<User>> GetAllUsersWithRolesAsync();
    Task SaveChangesAsync();

    Task<bool> GetByEmailAsync(string newEmail);

    Task<User?> GetUserWithRolesByEmailAsync(string email);
}