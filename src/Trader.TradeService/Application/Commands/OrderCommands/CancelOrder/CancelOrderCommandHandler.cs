using BuildingBlocks.Core.Cqrs.Commands;
using MediatR;
using Trader.TradeService.Domain.Order;
using Trader.TradeService.Domain.Order.Exceptions;

namespace Trader.TradeService.Application.Commands.OrderCommands.CancelOrder;

public class CancelOrderCommandHandler : ICommandHandler<CancelOrderCommand>
{
    private readonly IOrderCommandRepository _orderCommandRepository;

    public CancelOrderCommandHandler(IOrderCommandRepository orderCommandRepository)
    {
        _orderCommandRepository = orderCommandRepository;
    }
    public async Task<Unit> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderCommandRepository.GetAsync(request.Id);
        if (order == null)
        {
            throw new NotFoundOrderException("order not found");
        }

        order.Cancel();

        return Unit.Value;
    }
}