namespace DialedUp.Application.Users;

public interface IUserApplicationService
{
    Task<List<UserAdto>> GetAllUsers();

    Task<List<UserWithRolesAdto>> GetUsersWithRolesAsync();
    
    Task<UserWithRolesAdto> CreateUserAsync(CreateUserAdto createUserAdto);
}