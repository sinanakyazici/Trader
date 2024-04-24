using BuildingBlocks.Core.Event;

namespace Trader.TradeService.Application.Events.IntegrationEvents.OrderIntegrationEvents.SendSms;

public class SendSmsIntegrationEvent : IntegrationEvent
{
    public Guid OrderId { get; }
    public string GsmPhone { get; }
    public string Text { get; set; }

    public SendSmsIntegrationEvent(Guid orderId, string gsmPhone, string text)
    {
        OrderId = orderId;
        GsmPhone = gsmPhone;
        Text = text;
    }
}