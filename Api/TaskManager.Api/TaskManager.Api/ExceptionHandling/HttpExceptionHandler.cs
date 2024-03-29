using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TaskManager.Api.Exceptions;

namespace TaskManager.Api.ExceptionHandling
{
    public class HttpExceptionHandler(ILogger<HttpExceptionHandler> _logger) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            

            if (exception is HttpException httpException)
            {
                _logger.LogError(exception, httpException.Message);
                httpContext.Response.StatusCode = (int)httpException.StatusCodes;
                await httpContext.Response.WriteAsJsonAsync(new ProblemDetails
                {
                    Status = (int)httpException.StatusCodes,
                    Type = httpException.StatusCodes.ToString(),
                    Title = "Error",
                    Detail = exception.Message,
                    Instance = $"{httpContext.Request.Method} {httpContext.Request.Path}"
                });
                return true;
            }
            return false;
        }
    }
}
