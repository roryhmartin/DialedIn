using DialedUp.Application.ClockEntries;

namespace DialedUp.Application.Users;

public record UserAdto(
    int Id,
    string FirstName,
    string LastName,
    string? Email,
    bool? IsClockedIn,
    ClockEntryAdto? LastEntry
);