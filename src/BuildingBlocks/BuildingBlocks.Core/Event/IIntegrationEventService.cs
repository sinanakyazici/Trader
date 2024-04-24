namespace BuildingBlocks.Core.Event;

public interface IIntegrationEventService
{
    void Add(IntegrationEvent @event);
    Task DispatchEventsAsync(string transactionId);
}