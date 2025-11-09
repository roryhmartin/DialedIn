using DialedUp.Application.ClockEntries;
using DialedUp.Domain.Roles;
using DialedUp.Domain.UserRoles;
using DialedUp.Domain.Users;
using DialedUp.Persistance;
using DialedUp.Persistance.queries.user;
using Microsoft.EntityFrameworkCore;

namespace DialedUp.Application.Users;

public class UsersApplicationService
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
       {
           Id = u.id,
           FirstName = u.first_name,
           LastName = u.last_name,
           Email = u.email,
           IsClockedIn = u.IsClockedIn,
           LastEntry = u.ClockEntries
               .OrderByDescending(c => c.ClockInTime)
               .Select(c => new ClockEntryAdto(
                   c.Id,
                   c.ClockInTime,
                   c.ClockOutTime,
                   c.IsAmended)).FirstOrDefault()
       })
           .OrderByDescending(u => u.LastName)
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

    public async Task<UserWithRolesAdto> CreateUserAsync(string firstName, string lastName, string? email, string password, List<int> roles)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            throw new Exception("Email is required");
        }

        if (await _userRepository.GetByEmailAsync(email))
        {
            throw new Exception($"Email {email} already exists");
        }

        string? hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

        List<Role> newUserRoles = await _roleRepository.getRolesbyIdAsync(roles);

        User newUser = new User
        {
            first_name = firstName,
            last_name = lastName,
            email = email,
            password_hash = hashedPassword,
            created_date = DateTime.UtcNow,
            UserRoles = newUserRoles.Select(r => new UserRole { RoleId = r.Id }).ToList()
        };

        await _userRepository.AddUserAsync(newUser);

        User? createdUser = await _userRepository.GetUserWithRolesByEmailAsync(email);

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