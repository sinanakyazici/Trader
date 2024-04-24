using BuildingBlocks.Core.Cqrs.Commands;
using MediatR;
using Trader.TradeService.Domain.Channel;
using Trader.TradeService.Domain.Order;
using Trader.TradeService.Domain.Order.Exceptions;

namespace Trader.TradeService.Application.Commands.OrderCommands.PushNotification;

public class PushNotificationCommandHandler : ICommandHandler<PushNotificationCommand>
{
    private readonly IOrderCommandRepository _orderCommandRepository;
    private readonly ILogger<PushNotificationCommandHandler> _logger;

    public PushNotificationCommandHandler(IOrderCommandRepository orderCommandRepository, ILogger<PushNotificationCommandHandler> logger)
    {
        _orderCommandRepository = orderCommandRepository;
        _logger = logger;
    }
    public async Task<Unit> Handle(PushNotificationCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderCommandRepository.GetAsync(request.OrderId);
        if (order == null)
        {
            throw new NotFoundOrderException("order not found");
        }

        var orderNotification = order.OrderNotifications.First(x => x.ChannelId == Channel.PushNotification.Id);
        try
        {
            PushNotification(request.UserId, request.Text);
            orderNotification.Complete();
        }
        catch (Exception e)
        {
            orderNotification.Fail();
            _logger.LogError(e, e.Message);
        }

        return Unit.Value;
    }

    private void PushNotification(Guid userId, string text)
    {
        _logger.LogInformation("PUSH NOTIFICATION: {0} - {1}", userId, text);
    }
}