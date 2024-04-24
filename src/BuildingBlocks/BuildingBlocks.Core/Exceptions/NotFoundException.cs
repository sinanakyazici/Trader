using System.Net;

namespace BuildingBlocks.Core.Exceptions;

public class NotFoundException : BaseException
{
    public NotFoundException(string message, HttpStatusCode statusCode = HttpStatusCode.NotFound) : base(message, statusCode)
    {
    }
}