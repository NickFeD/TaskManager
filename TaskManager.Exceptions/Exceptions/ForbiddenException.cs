using System.Net;

namespace TaskManager.Core.Exceptions;

public class ForbiddenException : HttpException
{
    public ForbiddenException() : base(HttpStatusCode.Forbidden)
    {
    }

    public ForbiddenException(string message) : base(HttpStatusCode.Forbidden, message)
    {
    }

    public ForbiddenException(string message, Exception inner) : base(HttpStatusCode.Forbidden, message, inner)
    {
    }
}
