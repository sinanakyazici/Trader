using BuildingBlocks.Core.Event;
using MassTransit;
using MediatR;
using Trader.TradeService.Application.Commands.OrderCommands.SendSms;
using Trader.TradeService.Domain.Channel;
using Trader.TradeService.Domain.Order;
using Trader.TradeService.Domain.Order.Exceptions;

namespace Trader.TradeService.Application.Events.IntegrationEvents.OrderIntegrationEvents.SendSms;

public class SendSmsIntegrationEventHandler : IIntegrationEventHandler<SendSmsIntegrationEvent>
{
    private readonly IMediator _mediator;
    private readonly IOrderCommandRepository _orderCommandRepository;

    public SendSmsIntegrationEventHandler(IMediator mediator, IOrderCommandRepository orderCommandRepository)
    {
        _mediator = mediator;
        _orderCommandRepository = orderCommandRepository;
    }

    public async Task Consume(ConsumeContext<SendSmsIntegrationEvent> context)
    {
        var order = await _orderCommandRepository.GetAsync(context.Message.OrderId);
        if (order == null)
        {
            throw new NotFoundOrderException("order not found");
        }

        var orderNotification = order.OrderNotifications.First(x => x.ChannelId == Channel.Sms.Id);
        if (orderNotification.OrderNotificationStatus != OrderNotificationStatus.Completed)
        {
            await _mediator.Send(new SendSmsCommand(context.Message.OrderId, context.Message.GsmPhone, context.Message.Text));
        }
    }
}