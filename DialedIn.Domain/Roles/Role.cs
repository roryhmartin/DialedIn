using System.ComponentModel.DataAnnotations.Schema;
using DialedUp.Domain.UserRoles;

namespace DialedUp.Domain.Roles;

[Table("Roles")]
public class Role
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }

    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}