using SimpleTaskManager.Communication.Enums;
using TaskStatus = SimpleTaskManager.Communication.Enums.TaskStatus;

namespace SimpleTaskManager.Communication.Requests;

public class RequestGetAllTasksJson
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string Search { get; set; } = string.Empty;
    public PriorityType? Priority { get; set; }
    public TaskStatus? Status { get; set; }
    public DateTimeOffset? DueDate { get; set; }
    public SortDirectionType SortDirection { get; set; } = SortDirectionType.Asc;
}