namespace SimpleTaskManager.Communication.Responses;

public class ResponseRegisterTaskJson
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
}