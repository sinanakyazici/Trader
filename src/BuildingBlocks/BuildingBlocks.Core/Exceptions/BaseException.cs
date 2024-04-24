using System.Net;

namespace BuildingBlocks.Core.Exceptions;

public class BaseException : Exception
{
    public BaseException(string message, HttpStatusCode statusCode = HttpStatusCode.InternalServerError) : base(message)
    {
        StatusCode = statusCode;
    }

    public HttpStatusCode StatusCode { get; protected set; }
}