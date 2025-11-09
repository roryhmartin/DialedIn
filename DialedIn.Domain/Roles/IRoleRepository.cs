namespace DialedUp.Domain.Roles;

public interface IRoleRepository
{
    Task<List<Role>> getRolesbyIdAsync(List<int> roleIds);
}