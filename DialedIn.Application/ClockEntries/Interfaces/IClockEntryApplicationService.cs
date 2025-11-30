using DialedUp.Application.Users;

namespace DialedUp.Application.ClockEntries.Interfaces;

public interface IClockEntryApplicationService
{
    Task<UserAdto> ClockInOrOutAsync(int userId);
}