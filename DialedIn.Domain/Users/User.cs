using System.ComponentModel.DataAnnotations.Schema;
using DialedUp.Domain.ClockEntries;
using DialedUp.Domain.UserRoles;

namespace DialedUp.Domain.Users;

[Table("Users")]
public class User
{
    public int id { get; set; }
    public string first_name { get; set; }
    public string last_name { get; set; }
    public string? email { get; set; }
    public string password_hash { get; set; }
    public DateTime? created_date { get; set; }

    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    public ICollection<ClockEntry> ClockEntries { get; set; } = new List<ClockEntry>();
    public ICollection<ClockEntry> ApprovedClockEntries { get; set; } = new List<ClockEntry>();

    public bool IsClockedIn => ClockEntries.Any(c => c.ClockOutTime == null);

    public ClockEntry ClockIn()
    {
        if (IsClockedIn)
        {
            throw new InvalidOperationException("User is already clocked in.");
        }
        
        ClockEntry clockEntry = new ClockEntry
        {
            UserId = id,
            ClockInTime = DateTime.UtcNow,
        };
        
        ClockEntries.Add(clockEntry);

        return clockEntry;
    }

    public void ClockOut()
    {
        ClockEntry? openEntry = ClockEntries.FirstOrDefault((c => c.ClockOutTime == null));

        if (openEntry == null)
        {
            throw new InvalidOperationException("User is not clocked in.");
        }
        
        openEntry.ClockOutTime = DateTime.UtcNow;

        openEntry.TotalHours = (decimal)(openEntry.ClockOutTime.Value - openEntry.ClockInTime).TotalHours;
    }
}