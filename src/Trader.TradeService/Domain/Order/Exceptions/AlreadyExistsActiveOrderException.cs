using System.Net;
using BuildingBlocks.Core.Exceptions;

namespace Trader.TradeService.Domain.Order.Exceptions;

public class AlreadyExistsActiveOrderException : BadRequestException
{
    public AlreadyExistsActiveOrderException(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest) : base(message, statusCode)
    {
    }
}