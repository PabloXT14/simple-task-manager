using SimpleTaskManager.Communication.Requests;
using SimpleTaskManager.Communication.Responses;
using SimpleTaskManager.Domain.Entities;
using SimpleTaskManager.Infrastructure.Data;

namespace SimpleTaskManager.Application.UseCases.Tasks.Register;

public class RegisterTaskUseCase
{
    private readonly AppDbContext _dbContext;

    public RegisterTaskUseCase(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<ResponseRegisterTaskJson> ExecuteAsync(RequestRegisterTaskJson request)
    {
        var task = new TaskItem
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Description = request.Description,
            Priority = request.Priority,
            DueDate = request.DueDate,
            TaskStatus = request.TaskStatus,
        };
        
        await _dbContext.Tasks.AddAsync(task);
        await _dbContext.SaveChangesAsync();
        
        return new ResponseRegisterTaskJson
        {
            Id = task.Id,
            Name = task.Name
        };
    }
}