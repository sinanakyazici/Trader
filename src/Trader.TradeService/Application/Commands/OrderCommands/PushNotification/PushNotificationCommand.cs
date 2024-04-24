using BuildingBlocks.Core.Cqrs.Commands;

namespace Trader.TradeService.Application.Commands.OrderCommands.PushNotification;

public class PushNotificationCommand : ICommand
{
    public Guid OrderId { get; set; }
    public Guid UserId { get; set; }
    public string Text { get; set; }

    public PushNotificationCommand(Guid orderId, Guid userId, string text)
    {
        OrderId = orderId;
        UserId = userId;
        Text = text;
    }
}