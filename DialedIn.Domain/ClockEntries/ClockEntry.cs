using System.ComponentModel.DataAnnotations.Schema;
using DialedUp.Domain.Users;

namespace DialedUp.Domain.ClockEntries;

[Table("ClockEntries")]
public class ClockEntry
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    
    public DateTime ClockInTime { get; set; }
    public DateTime? ClockOutTime { get; set; }
    public int? ApprovedBy { get; set; }
    public User? ApprovedByUser { get; set; }
    public DateTime? ApprovedOn { get; set; }
    public bool IsAmended { get; set; }
    public Decimal? TotalHours { get; set; }
}