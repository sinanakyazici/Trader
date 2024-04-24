using System.Net;
using BuildingBlocks.Core.Exceptions;

namespace Trader.TradeService.Domain.Order.Exceptions;

public class NotFoundOrderUserException : BadRequestException
{
    public NotFoundOrderUserException(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest) : base(message, statusCode)
    {
    }
}