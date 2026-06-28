using SimpleTaskManager.Communication.Enums;
using TaskStatus = SimpleTaskManager.Communication.Enums.TaskStatus;

namespace SimpleTaskManager.Communication.Responses;

public class ResponseTaskJson
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public PriorityType Priority { get; set; }
    public DateTimeOffset DueDate { get; set; }
    public TaskStatus TaskStatus { get; set; }
}