using System.Net;

namespace TaskManager.Core.Exceptions;

public class BadRequestException : HttpException
{
    public BadRequestException() : base(HttpStatusCode.BadRequest)
    {
    }

    public BadRequestException(string message) : base(HttpStatusCode.BadRequest, message)
    {
    }

    public BadRequestException(string message, Exception inner) : base(HttpStatusCode.BadRequest, message, inner)
    {
    }
}
