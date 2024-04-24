using BuildingBlocks.Core.Cqrs.Queries;
using Trader.TradeService.Domain.Order;

namespace Trader.TradeService.Application.Queries.OrderQueries.GetOrderChannels;

public class GetOrderChannelsQueryHandler : IQueryHandler<GetOrderChannelsQuery, IEnumerable<OrderChannelViewModel>>
{
    private readonly IOrderQueryRepository _orderQueryRepository;

    public GetOrderChannelsQueryHandler(IOrderQueryRepository orderQueryRepository)
    {
        _orderQueryRepository = orderQueryRepository;
    }
    public async Task<IEnumerable<OrderChannelViewModel>> Handle(GetOrderChannelsQuery request, CancellationToken cancellationToken)
    {
        return await _orderQueryRepository.GetOrderChannels(request.OrderId);
    }
}