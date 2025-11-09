using DialedUp.Domain.Roles;
using Microsoft.EntityFrameworkCore;

namespace DialedUp.Persistance.queries.roles;

public class RoleRepository : IRoleRepository
{

    private AppDbContext _appDbContext;

    public RoleRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    
    public async Task<List<Role>> getRolesbyIdAsync(List<int> roleIds)
    {
        return await _appDbContext.Roles
            .Where(r => roleIds.Contains(r.Id))
            .ToListAsync();
    }   
}