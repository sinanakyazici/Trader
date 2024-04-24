namespace BuildingBlocks.Core.Event
{
    public interface IDomainEventService
    {
        Task DispatchEventsAsync();
    }
}