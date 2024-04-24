using BuildingBlocks.Data.EfCore;
using Microsoft.EntityFrameworkCore;
using Trader.TradeService.Domain.Channel;

namespace Trader.TradeService.Infrastructure.Data.CommandRepos;

public class ChannelCommandRepository : EfRepository<Channel>, IChannelCommandRepository
{
    public ChannelCommandRepository(BaseDbContext dbContext) : base(dbContext)
    {
    }

    public Task<bool> CheckChannelsExistAsync(IEnumerable<int> channelIds)
    {
        return Query().AnyAsync(x => channelIds.Contains(x.Id));
    }

    public async Task<IEnumerable<Channel>> GetAsync(IEnumerable<int> channelIds)
    {
        return await Query().Where(x => channelIds.Contains(x.Id)).ToListAsync();
    }
}