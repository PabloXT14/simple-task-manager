using Microsoft.AspNetCore.Mvc;

namespace SimpleTaskManager.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TaskController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new
        {
            message = "Hello from TaskController!"
        });
    }
}

