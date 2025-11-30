using DialedUp.Application.ClockEntries;
using DialedUp.Facade.ClockEntries.FDTO;

namespace DialedUp.Facade.ClockEntries;

public static class ClockEntryMappingExtensions
{
    public static ClockEntryFdto? ToFdto(this ClockEntryAdto? clockEntryAdto)
    {
        return clockEntryAdto == null
            ? null
            : new ClockEntryFdto(
                clockEntryAdto.Id,
                clockEntryAdto.ClockInTime,
                clockEntryAdto.ClockOutTime,
                clockEntryAdto.isAmended
            );
    }
}