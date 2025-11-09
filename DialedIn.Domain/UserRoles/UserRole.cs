using System.ComponentModel.DataAnnotations.Schema;
using DialedUp.Domain.Roles;
using DialedUp.Domain.Users;

namespace DialedUp.Domain.UserRoles;

[Table("UserRoles")]
public class UserRole
{
    public int UserId { get; set; }
    public User User { get; set; }
    
    public int RoleId { get; set; }
    public Role Roles { get; set; }
}