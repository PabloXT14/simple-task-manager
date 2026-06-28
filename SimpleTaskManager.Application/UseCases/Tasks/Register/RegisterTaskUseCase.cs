using SimpleTaskManager.Communication.Requests;
using SimpleTaskManager.Communication.Responses;
using SimpleTaskManager.Domain.Entities;
using SimpleTaskManager.Domain.Enums;
using SimpleTaskManager.Exception.ExceptionsBase;
using SimpleTaskManager.Infrastructure.Data;
using TaskStatus = SimpleTaskManager.Domain.Enums.TaskStatus;

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
        Validate(request);
        
        var task = new TaskItem
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Description = request.Description,
            DueDate = request.DueDate,
            Priority = (PriorityType)request.Priority,
            TaskStatus = (TaskStatus)request.TaskStatus,
        };
        
        await _dbContext.Tasks.AddAsync(task);
        await _dbContext.SaveChangesAsync();
        
        return new ResponseRegisterTaskJson
        {
            Id = task.Id,
            Name = task.Name
        };
    }

    private  void Validate(RequestRegisterTaskJson request)
    {
        var validator = new RegisterTaskValidator();
        
        var validationResult = validator.Validate(request);
        
        if (!validationResult.IsValid)
        {
            var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
            
            throw new ErrorOnValidationException(errorMessages);
        }
    }
}