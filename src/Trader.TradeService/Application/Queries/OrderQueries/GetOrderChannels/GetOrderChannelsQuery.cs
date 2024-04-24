using BuildingBlocks.Core.Cqrs.Queries;
using Trader.TradeService.Domain.Order;

namespace Trader.TradeService.Application.Queries.OrderQueries.GetOrderChannels;

public class GetOrderChannelsQuery : IQuery<IEnumerable<OrderChannelViewModel>>
{
    public Guid OrderId { get; set; }

    public GetOrderChannelsQuery(Guid orderId)
    {
        OrderId = orderId;
    }
}