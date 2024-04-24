using System.Net;

namespace BuildingBlocks.Core.Exceptions;

public class BadRequestException : BaseException
{
    public BadRequestException(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest) : base(message, statusCode)
    {
    }
}