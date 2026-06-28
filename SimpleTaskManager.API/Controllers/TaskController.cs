using Microsoft.AspNetCore.Mvc;
using SimpleTaskManager.Application.UseCases.Tasks.GetAll;
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
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register(
        [FromServices] RegisterTaskUseCase useCase,
        [FromBody] RequestRegisterTaskJson request
    )
    {
        var response = await useCase.Execute(request);
        
        return Created(string.Empty, response);
    }
    
    [HttpGet]
    [ProducesResponseType(typeof(ResponseGetAllTasksJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAll(
        [FromServices] GetAllTasksUseCase useCase,
        [FromQuery] RequestGetAllTasksJson request
    )
    {
        var response = await useCase.Execute(request);
        
        return Ok(response);
    }
}

