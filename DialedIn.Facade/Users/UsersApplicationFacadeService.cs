using DialedUp.Application.Users;
using DialedUp.Facade.ClockEntries;
using DialedUp.Facade.Users.Interfaces;

namespace DialedUp.Facade.Users;

public class UsersApplicationFacadeService : IUsersApplicationFacadeService
{
    private readonly IUserApplicationService _usersApplicationService;

    public UsersApplicationFacadeService(IUserApplicationService userApplicationService)
    {
        _usersApplicationService = userApplicationService;
    }

    public async Task<List<UserFdto>> GetAllUsersAsync()
    {
        List<UserAdto> users = await _usersApplicationService.GetAllUsers();

        return users.Select(u => new UserFdto(
                u.Id,
                u.FirstName,
                u.LastName,
                u.Email,
                u.IsClockedIn,
                u.LastEntry.ToFdto()))
            .ToList();
    }

    public async Task<UserWithRolesFdto> CreateUserAsync(CreateUserFdto createUserFdto)
    {
        UserWithRolesAdto createdUser = await _usersApplicationService.CreateUserAsync(new CreateUserAdto(
                createUserFdto.FirstName,
                createUserFdto.LastName,
                createUserFdto.Email,
                createUserFdto.Password,
                createUserFdto.RoleIds)

        );

        return new UserWithRolesFdto(
            createdUser.Id,
            createdUser.FirstName,
            createdUser.LastName,
            createdUser.Email,
            createdUser.Roles);
    }
}