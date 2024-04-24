using BuildingBlocks.Data.EfCore;
using Microsoft.EntityFrameworkCore;
using Trader.TradeService.Domain.User;

namespace Trader.TradeService.Infrastructure.Data.CommandRepos;

public class UserCommandRepository : EfRepository<User>, IUserCommandRepository
{
    public UserCommandRepository(BaseDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<bool> CheckUserExistsAsync(Guid userId)
    {
        return await Query().AnyAsync(x => x.Id == userId);
    }

    public async Task<User> GetAsync(Guid userId)
    {
        return await Query().FirstAsync(x => x.Id == userId);
    }
}