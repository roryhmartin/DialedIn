using DialedUp.Application.ClockEntries;
using DialedUp.Domain.Roles;
using DialedUp.Domain.UserRoles;
using DialedUp.Domain.Users;

namespace DialedUp.Application.Users;

public class UsersApplicationService : IUserApplicationService
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    

    public UsersApplicationService(IUserRepository userRepository, IRoleRepository roleRepository)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
    }

    public async Task<List<UserAdto>> GetAllUsers()
    {
       List<User> users = await _userRepository.GetAllUsersAsync();
       
       return users.Select(u => new UserAdto
       (
           u.id,
           u.first_name,
           u.last_name,
           u.email,
           u.IsClockedIn,
           u.ClockEntries
               .OrderByDescending(c => c.ClockInTime)
               .Select(c => new ClockEntryAdto(
                   c.Id,
                   c.ClockInTime,
                   c.ClockOutTime,
                   c.IsAmended)).FirstOrDefault()
       ))
           .OrderBy(u => u.LastName)
           .ToList();
    }

    public async Task<List<UserWithRolesAdto>> GetUsersWithRolesAsync()
    {
        List<User> usersWithRoles = await _userRepository.GetAllUsersWithRolesAsync();

        return usersWithRoles.Select(u => new UserWithRolesAdto(
            u.id,
            u.first_name,
            u.last_name,
            u.email,
            u.UserRoles.Select(ur => ur.Roles.Name).ToList())).ToList();
    }

    public async Task<UserWithRolesAdto> CreateUserAsync(CreateUserAdto createUserAdto)
    {
        if (string.IsNullOrWhiteSpace(createUserAdto.Email))
        {
            throw new Exception("Email is required");
        }

        if (await _userRepository.GetByEmailAsync(createUserAdto.Email))
        {
            throw new Exception($"Email {createUserAdto.Email} already exists");
        }

        string? hashedPassword = BCrypt.Net.BCrypt.HashPassword(createUserAdto.Password);

        List<Role> newUserRoles = await _roleRepository.getRolesbyIdAsync(createUserAdto.Roles);

        User newUser = new User
        {
            first_name = createUserAdto.FirstName,
            last_name = createUserAdto.LastName,
            email = createUserAdto.Email,
            password_hash = hashedPassword,
            created_date = DateTime.UtcNow,
            UserRoles = newUserRoles.Select(r => new UserRole { RoleId = r.Id }).ToList()
        };

        await _userRepository.AddUserAsync(newUser);

        User? createdUser = await _userRepository.GetUserWithRolesByEmailAsync(createUserAdto.Email);

        return new UserWithRolesAdto
        (
            createdUser.id,
            createdUser.first_name,
            createdUser.last_name,
            createdUser.email,
            createdUser.UserRoles.Select(ur => ur.Roles.Name).ToList()
        );
    }
}