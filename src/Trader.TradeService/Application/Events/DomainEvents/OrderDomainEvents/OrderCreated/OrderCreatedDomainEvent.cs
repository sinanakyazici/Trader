using MediatR;
using Trader.TradeService.Domain.Order;

namespace Trader.TradeService.Application.Events.DomainEvents.OrderDomainEvents.OrderCreated;

public class OrderCreatedDomainEvent : INotification
{
    public Order Order { get; }

    public OrderCreatedDomainEvent(Order order)
    {
        Order = order;
    }
}