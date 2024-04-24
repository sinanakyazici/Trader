using BuildingBlocks.Core.Cqrs.Queries;
using BuildingBlocks.Core.Exceptions;
using Trader.TradeService.Domain.User;

namespace Trader.TradeService.Application.Queries.UserQueries.GetUser;

public class GetUserQueryHandler : IQueryHandler<GetUserQuery, UserViewModel>
{
    private readonly IUserQueryRepository _userQueryRepository;

    public GetUserQueryHandler(IUserQueryRepository userQueryRepository)
    {
        _userQueryRepository = userQueryRepository;
    }

    public async Task<UserViewModel> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _userQueryRepository.GetUser(request.Id);
        if (user == null)
        {
            throw new NotFoundException($"{request.Id} user id is not found.");
        }

        return user;
    }
}