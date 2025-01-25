using System.Net;
using LessonService.Domain.Models.System;
using Microsoft.AspNetCore.Diagnostics;

namespace LessonService.WebApi.Exception;

public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : System.Exception, IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync( HttpContext httpContext,
        System.Exception exception,
        CancellationToken cancellationToken)
    {
        logger.LogError(exception,"An error occurred while processing the request.");
        var errorResponse = new ErrorResponse
        (
            exception.Message,
            exception.GetType().Name,
            exception switch
            {
                BadHttpRequestException => (int)HttpStatusCode.BadRequest,
                _ => (int)HttpStatusCode.InternalServerError
            }
        );
        httpContext.Response.StatusCode = errorResponse.StatusCode;
        await httpContext.Response.WriteAsJsonAsync(errorResponse, cancellationToken);
        return true;
    }
}