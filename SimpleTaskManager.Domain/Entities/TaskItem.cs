using SimpleTaskManager.Domain.Enums;
using TaskStatus = SimpleTaskManager.Domain.Enums.TaskStatus;

namespace SimpleTaskManager.Domain.Entities;

public class TaskItem
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public PriorityType Priority { get; set; }
    public DateTime DueDate { get; set; }
    public TaskStatus TaskStatus { get; set; }
}