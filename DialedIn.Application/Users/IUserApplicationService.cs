namespace DialedUp.Application.Users;

public interface IUserApplicationService
{
    Task<UserWithRolesAdto> CreateUserAsync(string firstName, string lastName, string? email, string password,
        List<int> roles);
}