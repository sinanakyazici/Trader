using BuildingBlocks.Core.Domain;

namespace Trader.TradeService.Domain.Order;

public class OrderNotification : AuditAggregateRoot<Guid>
{
    public Guid OrderId { get; private set; }
    public Guid UserId { get; private set; }
    public int ChannelId { get; private set; }
    public OrderNotificationStatus OrderNotificationStatus { get; private set; } = null!;
    public string Text { get; private set; } = null!;

    public Order Order { get; set; } = null!;

    protected OrderNotification() { }

    public OrderNotification(Guid orderId, Guid userId, int channelId , string text)
    {
        Id = Guid.NewGuid();
        OrderId = orderId;
        UserId = userId;
        ChannelId = channelId;
        OrderNotificationStatus = OrderNotificationStatus.Waiting;
        Text = text;
    }

    public void Complete()
    {
        OrderNotificationStatus = OrderNotificationStatus.Completed;
    }

    public void Fail()
    {
        OrderNotificationStatus = OrderNotificationStatus.Failed;
    }
}