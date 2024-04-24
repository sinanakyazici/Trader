using MediatR;

namespace BuildingBlocks.Core.Domain;

public interface IAggregateRoot : IEntity
{
    IReadOnlyCollection<INotification> DomainEvents { get; }
    void ClearDomainEvents();
}

public abstract class AggregateRoot<TId> : Entity<TId>, IAggregateRoot
{
    private readonly List<INotification> _domainEvents;

    public IReadOnlyCollection<INotification> DomainEvents => _domainEvents.AsReadOnly();

    protected AggregateRoot()
    {
        _domainEvents = new List<INotification>();
    }

    public void AddDomainEvent(INotification eventItem)
    {
        _domainEvents.Add(eventItem);
    }

    public void RemoveDomainEvent(INotification eventItem)
    {
        _domainEvents?.Remove(eventItem);
    }

    public void ClearDomainEvents()
    {
        _domainEvents?.Clear();
    }
}