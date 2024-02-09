using System.Net;

namespace TaskManager.Api.Exceptions
{

	[Serializable]
	public class HttpException : Exception
	{
		public HttpStatusCode StatusCodes { get; private set; }
		public HttpException(HttpStatusCode statusCodes) => StatusCodes = statusCodes;
        public HttpException(HttpStatusCode statusCodes, string message) : base(message)
            => StatusCodes = statusCodes;
        public HttpException(HttpStatusCode statusCodes, string message, Exception inner) : base(message, inner) 
            => StatusCodes = statusCodes;
    }
}
