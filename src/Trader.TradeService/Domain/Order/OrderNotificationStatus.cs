using BuildingBlocks.Core.Domain;

namespace Trader.TradeService.Domain.Order;

public class OrderNotificationStatus : Enumeration
{
    // order created
    public static readonly OrderNotificationStatus Waiting = new(1, nameof(Waiting));
    // order done
    public static readonly OrderNotificationStatus Completed = new(2, nameof(Completed));
    // cancel order
    public static readonly OrderNotificationStatus Failed = new(3, nameof(Failed));

    public OrderNotificationStatus(int id, string name) : base(id, name)
    {
    }
}