using BuildingBlocks.Core.Event;
using MassTransit;
using MediatR;
using Trader.TradeService.Application.Commands.OrderCommands.PushNotification;
using Trader.TradeService.Domain.Channel;
using Trader.TradeService.Domain.Order;
using Trader.TradeService.Domain.Order.Exceptions;

namespace Trader.TradeService.Application.Events.IntegrationEvents.OrderIntegrationEvents.PushNotification;

public class PushNotificationIntegrationEventHandler : IIntegrationEventHandler<PushNotificationIntegrationEvent>
{
    private readonly IMediator _mediator;
    private readonly IOrderCommandRepository _orderCommandRepository;

    public PushNotificationIntegrationEventHandler(IMediator mediator, IOrderCommandRepository orderCommandRepository)
    {
        _mediator = mediator;
        _orderCommandRepository = orderCommandRepository;
    }

    public async Task Consume(ConsumeContext<PushNotificationIntegrationEvent> context)
    {
        var order = await _orderCommandRepository.GetAsync(context.Message.OrderId);
        if (order == null)
        {
            throw new NotFoundOrderException("order not found");
        }

        var orderNotification = order.OrderNotifications.First(x => x.ChannelId == Channel.PushNotification.Id);
        if (orderNotification.OrderNotificationStatus != OrderNotificationStatus.Completed)
        {
            await _mediator.Send(new PushNotificationCommand(context.Message.OrderId, context.Message.UserId, context.Message.Text));
        }
    }
}