using BuildingBlocks.Core.Domain;

namespace Trader.TradeService.Domain.Order;

public interface IOrderCommandRepository : ICommandRepository<Order>
{
    Task<bool> CheckActiveOrderExistsAsync(Guid userId);
    Task<Order?> GetAsync(Guid id);
}