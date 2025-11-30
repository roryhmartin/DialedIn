using DialedUp.Application.ClockEntries;
using DialedUp.Facade.ClockEntries.FDTO;

namespace DialedUp.Facade.Users;

public record UserFdto(
    int Id,
    string FirstName,
    string LastName,
    string? Email,
    bool? IsClockedIn,
    ClockEntryFdto? LastEntry
);