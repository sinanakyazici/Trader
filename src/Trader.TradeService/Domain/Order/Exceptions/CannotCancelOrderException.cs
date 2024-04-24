using System.Net;
using BuildingBlocks.Core.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Trader.TradeService.Domain.Order.Exceptions;

public class CannotCancelOrderException : BadRequestException
{
    public CannotCancelOrderException(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest) : base(message, statusCode)
    {
    }
}