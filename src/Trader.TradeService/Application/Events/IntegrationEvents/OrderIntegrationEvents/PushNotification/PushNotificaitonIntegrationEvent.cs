using BuildingBlocks.Core.Event;

namespace Trader.TradeService.Application.Events.IntegrationEvents.OrderIntegrationEvents.PushNotification;

public class PushNotificationIntegrationEvent : IntegrationEvent
{
    public Guid OrderId { get; }
    public Guid UserId { get; set; }
    public string Text { get; set; }

    public PushNotificationIntegrationEvent(Guid orderId, Guid userId, string text)
    {
        OrderId = orderId;
        UserId = userId;
        Text = text;
    }
}