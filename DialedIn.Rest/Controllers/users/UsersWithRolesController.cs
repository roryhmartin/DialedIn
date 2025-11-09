using DialedUp.Application.Users;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersWithRolesController : ControllerBase
{
    private readonly UsersApplicationService _usersApplicationService;

    public UsersWithRolesController(UsersApplicationService usersApplicationService)
    {
        _usersApplicationService = usersApplicationService;
    }

    [HttpGet("users-with-roles")]
    public async Task<IActionResult> GetUsersWithRoles()
    {
        List<UserWithRolesAdto> users = await _usersApplicationService.GetUsersWithRolesAsync();
        return Ok(users);
    }
}