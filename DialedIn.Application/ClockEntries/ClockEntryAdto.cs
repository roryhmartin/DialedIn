namespace DialedUp.Application.ClockEntries;

public record ClockEntryAdto(
    int Id,
    DateTime ClockInTime,
    DateTime? ClockOutTime,
    bool? isAmended);