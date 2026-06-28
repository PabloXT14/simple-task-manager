using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SimpleTaskManager.Communication.Responses;
using SimpleTaskManager.Exception.ExceptionsBase;

namespace SimpleTaskManager.API.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is SimpleTaskManagerException)
        {
            HandleProjectException(context);
        }
        else
        {
            ThrowUnknownError(context);
        }
    }

    private void HandleProjectException(ExceptionContext context)
    {
        var simpleTaskManagerException = (SimpleTaskManagerException)context.Exception;
        var errorResponse = new ResponseErrorJson(simpleTaskManagerException.GetErrorMessages());

        context.Result = new ObjectResult(errorResponse);
        context.HttpContext.Response.StatusCode = (int)simpleTaskManagerException.GetStatusCode();
    }

    private void ThrowUnknownError(ExceptionContext context)
    {
        var errorResponse = new ResponseErrorJson(["Erro desconhecido. Por favor, tente novamente mais tarde."]);
        context.Result = new ObjectResult(errorResponse);
        context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
    }
}