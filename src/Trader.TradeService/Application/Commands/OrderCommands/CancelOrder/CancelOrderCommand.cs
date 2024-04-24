using BuildingBlocks.Core.Cqrs.Commands;

namespace Trader.TradeService.Application.Commands.OrderCommands.CancelOrder;

public class CancelOrderCommand : ICommand
{
    public Guid Id { get; set; }
}