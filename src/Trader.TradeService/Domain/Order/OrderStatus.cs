using BuildingBlocks.Core.Domain;

namespace Trader.TradeService.Domain.Order;

public class OrderStatus : Enumeration
{
    // order created
    public static readonly OrderStatus Active = new(1, nameof(Active));
    // order done
    public static readonly OrderStatus Completed = new(2, nameof(Completed));
    // cancel order
    public static readonly OrderStatus Cancelled = new(3, nameof(Cancelled));

    public OrderStatus(int id, string name) : base(id, name)
    {
    }
}