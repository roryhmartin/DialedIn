using DialedUp.Application.ClockEntries;

namespace DialedUp.Application.Users;

public class UserAdto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? Email { get; set; }
    
    public bool? IsClockedIn { get; set; }
    
    public ClockEntryAdto? LastEntry { get; set; }
}