using DialedUp.Facade.Users;

namespace DialedUp.Facade.ClockEntries.Interface;

public interface IClockEntryFacadeApplicationService
{
    Task<UserFdto> ClockInOrOutAsync(int userId);
}