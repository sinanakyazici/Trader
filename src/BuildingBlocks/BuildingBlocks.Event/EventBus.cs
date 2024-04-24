using BuildingBlocks.Core.Event;
using MassTransit;

namespace BuildingBlocks.Event;

public class EventBus : IEventBus
{
    private readonly IPublishEndpoint _publishEndpoint;

    public EventBus(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }


    public async Task PublishAsync<TEvent>(TEvent @event) where TEvent : IntegrationEvent
    {
        var type = @event.GetType();
        await _publishEndpoint.Publish(@event, type);
    }
}