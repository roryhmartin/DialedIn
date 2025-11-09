using DialedUp.Application.ClockEntries;
using DialedUp.Application.Users;
using DialedUp.Domain.ClockEntries;
using DialedUp.Domain.Users;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers;

[ApiController]
[Route("[controller]")]
public class ClockEntriesController : ControllerBase
{
    private readonly ClockEntryApplicationService _clockEntryApplicationService;

    public ClockEntriesController(ClockEntryApplicationService ClockEntryApplicationService)
    {
        _clockEntryApplicationService = ClockEntryApplicationService;
    }

    [HttpPost("toggle-clock/{userId}")]
    public async Task<IActionResult> ToggleClock(int userId)
    {
        UserAdto user = await _clockEntryApplicationService.ClockInOrOutAsync(userId);
        
        return Ok(user);
    }
}