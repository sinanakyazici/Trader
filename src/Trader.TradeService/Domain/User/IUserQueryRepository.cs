using BuildingBlocks.Core.Domain;

namespace Trader.TradeService.Domain.User;

public interface IUserQueryRepository : IQueryRepository
{
    Task<UserViewModel?> GetUser(Guid userId);
}