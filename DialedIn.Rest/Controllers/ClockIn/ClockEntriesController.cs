using DialedUp.Facade.ClockEntries.Interface;
using DialedUp.Facade.Users;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers;

[ApiController]
[Route("[controller]")]
public class ClockEntriesController : ControllerBase
{
    private readonly IClockEntryFacadeApplicationService _clockEntryFacadeApplicationService;

    public ClockEntriesController(IClockEntryFacadeApplicationService clockEntryFacadeApplicationService)
    {
        _clockEntryFacadeApplicationService = clockEntryFacadeApplicationService;
    }
    
    [HttpPost("toggle-clock/{userId}")]
    public async Task<IActionResult> ToggleClock(int userId)
    {
        UserFdto user = await _clockEntryFacadeApplicationService.ClockInOrOutAsync(userId);
        
        return Ok(user);
    }
}