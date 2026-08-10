using Microsoft.AspNetCore.Mvc;

namespace DevKnowledge.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HealthController : ControllerBase
{
    [HttpGet]
    public IActionResult Get() => Ok(new { status = "healthy", timestampUtc = DateTime.UtcNow });
}
