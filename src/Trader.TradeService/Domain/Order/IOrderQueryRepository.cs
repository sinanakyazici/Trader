using BuildingBlocks.Core.Domain;

namespace Trader.TradeService.Domain.Order;

public interface IOrderQueryRepository : IQueryRepository
{
    Task<IEnumerable<OrderChannelViewModel>> GetOrderChannels(Guid orderId);
    Task<OrderViewModel> GetUserActiveOrder(Guid userId);
}