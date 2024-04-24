using BuildingBlocks.Core.Event;
using MassTransit;
using MediatR;
using Trader.TradeService.Application.Commands.OrderCommands.SendEmail;
using Trader.TradeService.Domain.Channel;
using Trader.TradeService.Domain.Order;
using Trader.TradeService.Domain.Order.Exceptions;

namespace Trader.TradeService.Application.Events.IntegrationEvents.OrderIntegrationEvents.SendEmail;

public class SendEmailIntegrationEventHandler : IIntegrationEventHandler<SendEmailIntegrationEvent>
{
    private readonly IMediator _mediator;
    private readonly IOrderCommandRepository _orderCommandRepository;

    public SendEmailIntegrationEventHandler(IMediator mediator, IOrderCommandRepository orderCommandRepository)
    {
        _mediator = mediator;
        _orderCommandRepository = orderCommandRepository;
    }

    public async Task Consume(ConsumeContext<SendEmailIntegrationEvent> context)
    {
        var order = await _orderCommandRepository.GetAsync(context.Message.OrderId);
        if (order == null)
        {
            throw new NotFoundOrderException("order not found");
        }

        var orderNotification = order.OrderNotifications.First(x => x.ChannelId == Channel.Email.Id);
        if (orderNotification.OrderNotificationStatus != OrderNotificationStatus.Completed)
        {
            await _mediator.Send(new SendEmailCommand(context.Message.OrderId, context.Message.Email, context.Message.Text));
        }
    }
}