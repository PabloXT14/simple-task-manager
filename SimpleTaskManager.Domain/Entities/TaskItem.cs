using SimpleTaskManager.Domain.Enums;

namespace SimpleTaskManager.Domain.Entities;

public class TaskItem
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public PriorityType Priority { get; set; }
    public DateTime DueDate { get; set; }
    public Status Status { get; set; }
}