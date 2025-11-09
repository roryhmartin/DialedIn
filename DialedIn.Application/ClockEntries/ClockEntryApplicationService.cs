using DialedUp.Application.Users;
using DialedUp.Domain.ClockEntries;
using DialedUp.Domain.Users;
using DialedUp.Persistance;
using Microsoft.EntityFrameworkCore;

namespace DialedUp.Application.ClockEntries;

public class ClockEntryApplicationService
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
        {
            Id = user.id,
            FirstName = user.first_name,
            LastName = user.last_name,
            Email = user.email,
            IsClockedIn = user.IsClockedIn,
            LastEntry = lastEntry
        };
    }
}