using System.Net;
using BuildingBlocks.Core.Exceptions;

namespace Trader.TradeService.Domain.Order.Exceptions;

public class NotFoundOrderException : NotFoundException
{
    public NotFoundOrderException(string message, HttpStatusCode statusCode = HttpStatusCode.NotFound) : base(message, statusCode)
    {
    }
}