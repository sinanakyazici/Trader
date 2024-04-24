using BuildingBlocks.Core.Event;

namespace Trader.TradeService.Application.Events.IntegrationEvents.OrderIntegrationEvents.SendEmail;

public class SendEmailIntegrationEvent : IntegrationEvent
{
    public Guid OrderId { get; }
    public string Email { get; }
    public string Text { get; set; }

    public SendEmailIntegrationEvent(Guid orderId, string email, string text)
    {
        OrderId = orderId;
        Email = email;
        Text = text;
    }
}