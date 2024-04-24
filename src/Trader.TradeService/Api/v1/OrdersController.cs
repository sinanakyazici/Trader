using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Trader.TradeService.Application.Commands.OrderCommands.CancelOrder;
using Trader.TradeService.Application.Commands.OrderCommands.CreateOrder;
using Trader.TradeService.Application.Queries.OrderQueries.GetOrderChannels;
using Trader.TradeService.Application.Queries.OrderQueries.GetUserActiveOrder;
using Trader.TradeService.Domain.Order;

namespace Trader.TradeService.Api.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrdersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [MapToApiVersion("1.0")]
    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<ActionResult> CreateOrder(CreateOrderCommand command)
    {
        await _mediator.Send(command);
        return Ok();
    }


    [MapToApiVersion("1.0")]
    [HttpPatch("cancel")]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<ActionResult> CancelOrder(CancelOrderCommand command)
    {
        await _mediator.Send(command);
        return Ok();
    }

    [MapToApiVersion("1.0")]
    [HttpGet("user-active-order")]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(OrderViewModel), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<OrderViewModel>> GetUserActiveOrder([FromQuery] GetUserActiveOrderQuery query)
    {
        var data = await _mediator.Send(query);
        return Ok(data);
    }


    [MapToApiVersion("1.0")]
    [HttpGet("{orderId:guid}/channels")]
    [ProducesResponseType(typeof(IEnumerable<OrderChannelViewModel>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IEnumerable<OrderChannelViewModel>>> GetOrderChannels(Guid orderId)
    {
        var data = await _mediator.Send(new GetOrderChannelsQuery(orderId));
        return Ok(data);
    }
}