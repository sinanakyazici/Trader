using BuildingBlocks.Core.Event;
using MediatR;
using Trader.TradeService.Application.Events.IntegrationEvents.OrderIntegrationEvents.PushNotification;
using Trader.TradeService.Application.Events.IntegrationEvents.OrderIntegrationEvents.SendEmail;
using Trader.TradeService.Application.Events.IntegrationEvents.OrderIntegrationEvents.SendSms;
using Trader.TradeService.Domain.Channel;
using Trader.TradeService.Domain.User;

namespace Trader.TradeService.Application.Events.DomainEvents.OrderDomainEvents.OrderCreated;

public class OrderCreatedDomainEventHandler : INotificationHandler<OrderCreatedDomainEvent>
{
    private readonly IUserCommandRepository _userCommandRepository;
    private readonly IIntegrationEventService _integrationEventService;

    public OrderCreatedDomainEventHandler(
        IUserCommandRepository userCommandRepository,
        IIntegrationEventService integrationEventService)
    {
        _userCommandRepository = userCommandRepository;
        _integrationEventService = integrationEventService;
    }

    public async Task Handle(OrderCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        var user = await _userCommandRepository.GetAsync(notification.Order.UserId);
        foreach (var orderNotification in notification.Order.OrderNotifications)
        {
            if (orderNotification.ChannelId == Channel.Sms.Id)
            {
                var sendSmsIntegrationEvent = new SendSmsIntegrationEvent(notification.Order.Id, user.GsmPhone, orderNotification.Text);
                _integrationEventService.Add(sendSmsIntegrationEvent);
            }
            else if (orderNotification.ChannelId == Channel.Email.Id)
            {
                var sendEmailIntegrationEvent = new SendEmailIntegrationEvent(notification.Order.Id, user.Email, orderNotification.Text);
                _integrationEventService.Add(sendEmailIntegrationEvent);
            }
            else if (orderNotification.ChannelId == Channel.PushNotification.Id)
            {
                var pushNotificationIntegrationEvent = new PushNotificationIntegrationEvent(notification.Order.Id, user.Id, orderNotification.Text);
                _integrationEventService.Add(pushNotificationIntegrationEvent);
            }
        }
    }
}