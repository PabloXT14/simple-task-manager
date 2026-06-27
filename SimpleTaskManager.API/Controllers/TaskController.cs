using Microsoft.AspNetCore.Mvc;
using SimpleTaskManager.Application.UseCases.Tasks.Register;
using SimpleTaskManager.Communication.Requests;
using SimpleTaskManager.Communication.Responses;

namespace SimpleTaskManager.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TaskController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisterTaskJson), StatusCodes.Status201Created)]
    public async Task<IActionResult> Register(
        [FromServices] RegisterTaskUseCase useCase,
        [FromBody] RequestRegisterTaskJson request
    )
    {
        var response = await useCase.ExecuteAsync(request);
        
        return Created(string.Empty, response);
    }
}

