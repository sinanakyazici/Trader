using BuildingBlocks.Core.Domain;
using BuildingBlocks.Core.Event;

namespace BuildingBlocks.Event;

public class IntegrationEventService : IIntegrationEventService
{
    private readonly IEventBus _eventBus;
    private readonly IUnitOfWork _unitOfWork;
    private readonly Dictionary<string, List<IntegrationEvent>> _integrationEvents = new();

    public IntegrationEventService(IEventBus eventBus, IUnitOfWork unitOfWork)
    {
        _eventBus = eventBus;
        _unitOfWork = unitOfWork;
    }

    public void Add(IntegrationEvent @event)
    {
        var transactionId = _unitOfWork.TransactionId;

        if (_integrationEvents.ContainsKey(transactionId))
        {
            _integrationEvents[transactionId].Add(@event);
        }
        else
        {
            _integrationEvents[transactionId] = new List<IntegrationEvent> { @event };
        }
    }

    public async Task DispatchEventsAsync(string transactionId)
    {
        if (_integrationEvents.ContainsKey(transactionId))
        {
            var events = _integrationEvents[transactionId];
            foreach (var integrationEvent in events)
            {
                await _eventBus.PublishAsync(integrationEvent);
            }
        }
    }
}