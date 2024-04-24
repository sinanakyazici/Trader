using BuildingBlocks.Core.Cqrs.Queries;
using Trader.TradeService.Domain.Order;

namespace Trader.TradeService.Application.Queries.OrderQueries.GetUserActiveOrder;

public class GetUserActiveOrderQuery : IQuery<OrderViewModel>
{
    public Guid UserId { get; set; }
}