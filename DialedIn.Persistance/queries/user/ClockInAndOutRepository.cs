using DialedUp.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace DialedUp.Persistance.queries.user;

public class ClockInAndOutRepository : IClockInAndOutRepository
{
    private readonly AppDbContext _dbContext;

    public ClockInAndOutRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        return await _dbContext.Users
            .Include(u => u.ClockEntries)
            .FirstOrDefaultAsync(u => u.id == id);
    }

    public async Task SaveAsync(User user)
    {
        await _dbContext.SaveChangesAsync();
    }
}