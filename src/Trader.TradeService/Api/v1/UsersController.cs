using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Trader.TradeService.Application.Queries.UserQueries.GetUser;
using Trader.TradeService.Domain.User;

namespace Trader.TradeService.Api.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [MapToApiVersion("1.0")]
    [HttpGet("{userId:guid}")]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(UserViewModel), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<UserViewModel>> GetUser(Guid userId)
    {
        var data = await _mediator.Send(new GetUserQuery(userId));
        return Ok(data);
    }
}