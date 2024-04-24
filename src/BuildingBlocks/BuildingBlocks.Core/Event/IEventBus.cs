namespace BuildingBlocks.Core.Event;

public interface IEventBus
{
    Task PublishAsync<TEvent>(TEvent @event) where TEvent : IntegrationEvent;
}
