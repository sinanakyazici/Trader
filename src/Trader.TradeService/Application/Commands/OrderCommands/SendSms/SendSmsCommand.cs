using BuildingBlocks.Core.Cqrs.Commands;

namespace Trader.TradeService.Application.Commands.OrderCommands.SendSms;

public class SendSmsCommand : ICommand
{
    public Guid OrderId { get; set; }
    public string GsmPhone { get; set; }
    public string Text { get; set; }

    public SendSmsCommand(Guid orderId, string gsmPhone, string text)
    {
        OrderId = orderId;
        GsmPhone = gsmPhone;
        Text = text;
    }
}