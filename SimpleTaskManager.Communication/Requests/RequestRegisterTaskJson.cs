using SimpleTaskManager.Domain.Enums;
using TaskStatus = SimpleTaskManager.Domain.Enums.TaskStatus;

namespace SimpleTaskManager.Communication.Requests;

public class RequestRegisterTaskJson
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public PriorityType Priority { get; set; }
    public DateTime DueDate { get; set; }
    public TaskStatus TaskStatus { get; set; }
}