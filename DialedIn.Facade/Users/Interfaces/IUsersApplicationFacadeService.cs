namespace DialedUp.Facade.Users.Interfaces;

public interface IUsersApplicationFacadeService
{
    Task<List<UserFdto>> GetAllUsersAsync();

    Task<UserWithRolesFdto> CreateUserAsync(CreateUserFdto createUserFdto);
}