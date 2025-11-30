using DialedUp.Application.Users;
using DialedUp.Domain.Users;
using DialedUp.Facade.Users;
using DialedUp.Facade.Users.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers.users;


[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUsersApplicationFacadeService _usersApplicationFacadeService;

    public UsersController(IUsersApplicationFacadeService usersApplicationFacadeService)
    {
        _usersApplicationFacadeService = usersApplicationFacadeService;
    }

    [HttpGet("users")]
    public async Task<IActionResult> GetAllUsers()
    {
        List<UserFdto> users = await _usersApplicationFacadeService.GetAllUsersAsync();

        return Ok(users);
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserFdto createUserFdto)
    {
        UserWithRolesFdto createdUser = await _usersApplicationFacadeService.CreateUserAsync(createUserFdto);

        return CreatedAtAction(
            nameof(GetAllUsers), 
            new { id = createdUser.Id }, 
            createdUser);
    }
}