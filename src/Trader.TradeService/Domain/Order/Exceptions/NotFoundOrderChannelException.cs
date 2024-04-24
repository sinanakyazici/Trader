using System.Net;
using BuildingBlocks.Core.Exceptions;

namespace Trader.TradeService.Domain.Order.Exceptions;

public class NotFoundOrderChannelException : BadRequestException
{
    public NotFoundOrderChannelException(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest) : base(message, statusCode)
    {
    }
}