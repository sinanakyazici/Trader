using BuildingBlocks.Core.Domain;
using Trader.TradeService.Application.Events.DomainEvents.OrderDomainEvents.OrderCreated;
using Trader.TradeService.Domain.Order.Exceptions;

namespace Trader.TradeService.Domain.Order;

public class Order : AuditAggregateRoot<Guid>
{
    public Guid UserId { get; }
    public int DayOfMonth { get; }
    public decimal Amount { get; }
    public OrderStatus OrderStatus { get; private set; }
    public ICollection<OrderChannel> OrderChannels { get; }
    public ICollection<OrderNotification> OrderNotifications { get; set; }

    protected Order()
    {
        OrderStatus = OrderStatus.Active;
        OrderChannels = new List<OrderChannel>();
        OrderNotifications = new List<OrderNotification>();
    }

    public Order(Guid id, Guid userId, int dayOfMonth, IReadOnlyCollection<int> channelIds, decimal amount)
    {
        if (id == Guid.Empty)
        {
            throw new EmptyOrderIdException("The order id cannot be empty.");
        }

        if (dayOfMonth is < 1 or > 28)
        {
            throw new OutRangeOfOrderDayException("The order amount cannot be less than 100 TL.");
        }

        if (amount < 100)
        {
            throw new MinimumOrderAmountException("The order amount cannot be less than 100 TL.");
        }

        if (amount > 20000)
        {
            throw new MaximumOrderAmountException("The order amount cannot be greater than 20.000 TL.");
        }

        if (!channelIds.Any())
        {
            throw new NotFoundOrderChannelException("The order must has a channel.");
        }

        Id = id;
        UserId = userId;
        DayOfMonth = dayOfMonth;
        Amount = amount;
        OrderStatus = OrderStatus.Active;

        OrderChannels = channelIds.Distinct().Select(x => new OrderChannel(id, x)).ToList();
        const string textMessage = "Test mesaj";
        OrderNotifications = channelIds.Distinct().Select(x => new OrderNotification(id, userId, x, textMessage)).ToList();
        AddDomainEvent(new OrderCreatedDomainEvent(this));
    }

    public void Cancel()
    {
        if (OrderStatus == OrderStatus.Completed)
        {
            throw new CannotCancelOrderException("The order that has already been completed cannot be cancelled.");
        }

        OrderStatus = OrderStatus.Cancelled;
    }
}