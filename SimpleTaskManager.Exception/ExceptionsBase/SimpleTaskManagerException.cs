using System.Net;

namespace SimpleTaskManager.Exception.ExceptionsBase;

public abstract class SimpleTaskManagerException : SystemException
{
    protected SimpleTaskManagerException(string message) : base(message) { }
    
    public abstract List<string> GetErrorMessages();
    public abstract HttpStatusCode GetStatusCode();
}