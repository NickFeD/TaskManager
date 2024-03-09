using System.Net;

namespace TaskManager.Api.Exceptions
{
    public class NotFoundException : HttpException
    {
        public NotFoundException() : base(HttpStatusCode.NotFound)
        {
        }

        public NotFoundException(string message) : base(HttpStatusCode.NotFound, message)
        {
        }

        public NotFoundException(string message, Exception inner) : base(HttpStatusCode.NotFound, message, inner)
        {
        }
    }
}
