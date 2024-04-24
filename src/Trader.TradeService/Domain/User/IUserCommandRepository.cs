using BuildingBlocks.Core.Domain;

namespace Trader.TradeService.Domain.User;

public interface IUserCommandRepository : ICommandRepository<User>
{
    Task<bool> CheckUserExistsAsync(Guid userId);
    Task<User> GetAsync(Guid userId);
}