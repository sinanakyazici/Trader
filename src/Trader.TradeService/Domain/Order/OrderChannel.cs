using BuildingBlocks.Core.Domain;

namespace Trader.TradeService.Domain.Order;

public class OrderChannel : AuditEntity<Guid>
{
    public Guid OrderId { get; }
    public int ChannelId { get; }

    public Order Order { get; set; } = null!;

    protected OrderChannel() { }

    public OrderChannel(Guid orderId, int channelId)
    {
        Id = Guid.NewGuid();
        OrderId = orderId;
        ChannelId = channelId;
    }
}