using BuildingBlocks.Core.Cqrs.Queries;
using Trader.TradeService.Domain.User;

namespace Trader.TradeService.Application.Queries.UserQueries.GetUser;

public class GetUserQuery : IQuery<UserViewModel>
{
    public GetUserQuery(Guid userId)
    {
        Id = userId;
    }

    public Guid Id { get; set; }
}