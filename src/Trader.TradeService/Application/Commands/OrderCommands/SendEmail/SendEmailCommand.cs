using BuildingBlocks.Core.Cqrs.Commands;

namespace Trader.TradeService.Application.Commands.OrderCommands.SendEmail;

public class SendEmailCommand : ICommand
{
    public Guid OrderId { get; set; }
    public string Email { get; set; }
    public string Text { get; set; }

    public SendEmailCommand(Guid orderId, string email, string text)
    {
        OrderId = orderId;
        Email = email;
        Text = text;
    }
}