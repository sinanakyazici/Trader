using BuildingBlocks.Core.Cqrs.Queries;
using Trader.TradeService.Domain.Order;
using Trader.TradeService.Domain.Order.Exceptions;

namespace Trader.TradeService.Application.Queries.OrderQueries.GetUserActiveOrder;

public class GetUserActiveOrderQueryHandler : IQueryHandler<GetUserActiveOrderQuery, OrderViewModel>
{
    private readonly IOrderQueryRepository _orderQueryRepository;

    public GetUserActiveOrderQueryHandler(IOrderQueryRepository orderQueryRepository)
    {
        _orderQueryRepository = orderQueryRepository;
    }

    public async Task<OrderViewModel> Handle(GetUserActiveOrderQuery request, CancellationToken cancellationToken)
    {
        var activeOrder = await _orderQueryRepository.GetUserActiveOrder(request.UserId);
        if (activeOrder == null)
        {
            throw new NotFoundOrderException("There is no active order.");
        }

        return activeOrder;
    }
}