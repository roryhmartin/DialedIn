using DialedUp.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace DialedUp.Persistance.queries.user;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _appDbContext;

    public UserRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<List<User>> GetAllUsersAsync()
    {
        return await _appDbContext.Users
            .Include(u => u.ClockEntries)
            .AsSplitQuery()
            .ToListAsync();
    }

    public async Task<User?> GetUserByIdAsync(int id)
    {
        return await _appDbContext.Users
            .FirstOrDefaultAsync(u => u.id == id);
    }

    public async Task AddUserAsync(User user)
    {
        await _appDbContext.AddAsync(user);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task UpdateUserAsync(User user)
    {
         _appDbContext.Update(user);
         await _appDbContext.SaveChangesAsync();
    }

    public async Task<List<User>> GetAllUsersWithRolesAsync()
    {
        return await _appDbContext.Users
            .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Roles)
            .ToListAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _appDbContext.SaveChangesAsync();
    }

    public async Task<bool> GetByEmailAsync(string newEmail)
    {
        return await _appDbContext.Users.AnyAsync(u => u.email == newEmail);
    }
    
    public async Task<User?> GetUserWithRolesByEmailAsync(string email)
    {
        return await _appDbContext.Users
            .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Roles)
            .FirstOrDefaultAsync(u => u.email == email);
    }
}