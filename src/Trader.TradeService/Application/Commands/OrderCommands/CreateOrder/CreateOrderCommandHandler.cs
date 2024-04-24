using BuildingBlocks.Core.Cqrs.Commands;
using MediatR;
using Trader.TradeService.Domain.Channel;
using Trader.TradeService.Domain.Order;
using Trader.TradeService.Domain.Order.Exceptions;
using Trader.TradeService.Domain.User;

namespace Trader.TradeService.Application.Commands.OrderCommands.CreateOrder;

public class CreateOrderCommandHandler : ICommandHandler<CreateOrderCommand>
{
    private readonly IOrderCommandRepository _orderCommandRepository;
    private readonly IUserCommandRepository _userCommandRepository;
    private readonly IChannelCommandRepository _channelCommandRepository;

    public CreateOrderCommandHandler(
        IOrderCommandRepository orderCommandRepository,
        IUserCommandRepository userCommandRepository,
        IChannelCommandRepository channelCommandRepository)
    {
        _orderCommandRepository = orderCommandRepository;
        _userCommandRepository = userCommandRepository;
        _channelCommandRepository = channelCommandRepository;
    }

    public async Task<Unit> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = new Order(request.Id, request.UserId, request.DayOfMonth, request.ChannelIds.ToList(), request.Amount);
        var userExists = await _userCommandRepository.CheckUserExistsAsync(order.UserId);
        if (!userExists)
        {
            throw new NotFoundOrderUserException($"The order user cannot be found. User Id: `{order.UserId}`");
        }

        var activeOrderExists = await _orderCommandRepository.CheckActiveOrderExistsAsync(order.UserId);
        if (activeOrderExists)
        {
            throw new AlreadyExistsActiveOrderException("You cannot place a new order while there is another active order.");
        }

        var channelExist = await _channelCommandRepository.CheckChannelsExistAsync(order.OrderChannels.Select(x => x.ChannelId));
        if (!channelExist)
        {
            throw new InvalidOrderChannelException("The one or more of the given order channels is invalid.");
        }

        await _orderCommandRepository.AddAsync(order, cancellationToken);

        return Unit.Value;
    }
}