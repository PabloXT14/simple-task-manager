namespace SimpleTaskManager.Communication.Responses;

public class ResponseGetAllTasksJson
{
    public List<ResponseTaskJson> Data { get; set; } = [];
    
    public PaginationMetadata Pagination { get; set; } = default!;
}