namespace DialedUp.Facade.ClockEntries.FDTO;

public record ClockEntryFdto(
    int Id,
    DateTime ClockInTime,
    DateTime? ClockOutTime,
    bool? isAmended);