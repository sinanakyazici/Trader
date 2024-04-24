using System.Net;
using BuildingBlocks.Core.Exceptions;

namespace Trader.TradeService.Domain.Order.Exceptions;

public class EmptyOrderIdException : BadRequestException
{
    public EmptyOrderIdException(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest) : base(message, statusCode)
    {
    }
}