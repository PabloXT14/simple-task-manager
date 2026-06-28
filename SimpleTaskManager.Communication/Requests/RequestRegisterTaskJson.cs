using SimpleTaskManager.Communication.Enums;
using TaskStatus = SimpleTaskManager.Communication.Enums.TaskStatus;

namespace SimpleTaskManager.Communication.Requests;

public class RequestRegisterTaskJson
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public DateTimeOffset DueDate { get; set; }
    public PriorityType Priority { get; set; }
    public TaskStatus TaskStatus { get; set; }
}