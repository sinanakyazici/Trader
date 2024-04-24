using BuildingBlocks.Data.EfCore;
using Microsoft.EntityFrameworkCore;
using Trader.TradeService.Domain.Order;

namespace Trader.TradeService.Infrastructure.Data.CommandRepos;

public class OrderCommandRepository : EfRepository<Order>, IOrderCommandRepository
{
    public OrderCommandRepository(BaseDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<bool> CheckActiveOrderExistsAsync(Guid userId)
    {
        return await Query().AnyAsync(x => x.UserId == userId && x.OrderStatus == OrderStatus.Active);
    }

    public async Task<Order?> GetAsync(Guid id)
    {
        return await Query().Include(x => x.OrderChannels).Include(x => x.OrderNotifications).FirstOrDefaultAsync(x => x.Id == id);
    }
}