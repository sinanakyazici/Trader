using BuildingBlocks.Core.Domain;

namespace Trader.TradeService.Domain.Channel;

public interface IChannelCommandRepository : ICommandRepository<Channel>
{
    Task<bool> CheckChannelsExistAsync(IEnumerable<int> channelIds);
    Task<IEnumerable<Channel>> GetAsync(IEnumerable<int> channelIds);
}