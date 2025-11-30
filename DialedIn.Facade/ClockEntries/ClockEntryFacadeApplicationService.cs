using DialedUp.Application.ClockEntries.Interfaces;
using DialedUp.Application.Users;
using DialedUp.Facade.ClockEntries.Interface;
using DialedUp.Facade.Users;

namespace DialedUp.Facade.ClockEntries;

public class ClockEntryFacadeApplicationService : IClockEntryFacadeApplicationService
{
    private readonly IClockEntryApplicationService _clockEntryApplicationService;

    public ClockEntryFacadeApplicationService(IClockEntryApplicationService clockEntryApplicationService)
    {
        _clockEntryApplicationService = clockEntryApplicationService;
    }
    
    public async Task<UserFdto> ClockInOrOutAsync(int userId)
    {
        UserAdto userAdto = await _clockEntryApplicationService.ClockInOrOutAsync(userId);
        
        return new UserFdto
        (
            userAdto.Id,
            userAdto.FirstName,
            userAdto.LastName,
            userAdto.Email,
            userAdto.IsClockedIn,
            userAdto.LastEntry.ToFdto()
        );
    }
}