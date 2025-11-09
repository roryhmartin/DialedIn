namespace DialedUp.Domain.Users;

public interface IClockInAndOutRepository
{
    Task<User?> GetByIdAsync(int id);
    Task SaveAsync(User user);
}