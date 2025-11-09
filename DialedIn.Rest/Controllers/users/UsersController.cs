using DialedUp.Application.Users;
using DialedUp.Domain.Users;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers.users;


[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly UsersApplicationService _usersApplicationService;

    public UsersController(UsersApplicationService usersApplicationService)
    {
        _usersApplicationService = usersApplicationService;
    }

    [HttpGet("users")]
    public async Task<IActionResult> GetAllUsers()
    {
        List<UserAdto> users = await _usersApplicationService.GetAllUsers();

        return Ok(users);
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserRequestDto createUserRequestDto)
    {
        UserWithRolesAdto createdUser = await _usersApplicationService.CreateUserAsync(
            createUserRequestDto.FirstName,
            createUserRequestDto.LastName, 
            createUserRequestDto.Email,
            createUserRequestDto.Password,
            createUserRequestDto.RoleIds);

        return CreatedAtAction(
            nameof(GetAllUsers), 
            new { id = createdUser.Id }, 
            createdUser);
    }
    
    //TODO: move this to facade layer
    public class CreateUserRequestDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<int> RoleIds { get; set; }
    }
}