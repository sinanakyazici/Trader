using BuildingBlocks.Core.Cqrs.Commands;

namespace Trader.TradeService.Application.Commands.OrderCommands.CreateOrder;

public class CreateOrderCommand : ICommand
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public int DayOfMonth { get; set; }
    public decimal Amount { get; set; }
    public IEnumerable<int> ChannelIds { get; set; } = new List<int>();
}