using DialedUp.Application.ClockEntries.Interfaces;
using DialedUp.Application.Users;
using DialedUp.Domain.Users;

namespace DialedUp.Application.ClockEntries;

public class ClockEntryApplicationService : IClockEntryApplicationService
{
    private readonly IClockInAndOutRepository _clockInAndOutRepository;

    public ClockEntryApplicationService(IClockInAndOutRepository clockInAndOutRepository)
    {
        _clockInAndOutRepository = clockInAndOutRepository;
    }

    public async Task<UserAdto> ClockInOrOutAsync(int userId)
    {
        User? user = await _clockInAndOutRepository.GetByIdAsync(userId);

        if (user == null)
            throw new Exception("User not found");

        if (user.IsClockedIn)
        {
            user.ClockOut();
        }
        else
        {
            user.ClockIn();
        }

        await _clockInAndOutRepository.SaveAsync(user);

        ClockEntryAdto? lastEntry = user.ClockEntries
            .OrderByDescending(c => c.ClockInTime)
            .Select(c => new ClockEntryAdto(
                c.Id,
                c.ClockInTime,
                c.ClockOutTime,
                c.IsAmended))
            .FirstOrDefault();

        return new UserAdto
        (
            user.id,
            user.first_name,
            user.last_name,
            user.email,
            user.IsClockedIn,
            lastEntry
        );
    }
}