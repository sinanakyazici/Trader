using MassTransit;

namespace BuildingBlocks.Core.Event;

public interface IIntegrationEventHandler<in TIntegrationEvent> : IConsumer<TIntegrationEvent> where TIntegrationEvent : IntegrationEvent
{
}