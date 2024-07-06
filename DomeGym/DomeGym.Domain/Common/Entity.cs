namespace DomeGym.Domain.Common;

public abstract class Entity
{
    protected readonly List<IDomainEvent> _domainEvents = [];

    protected Entity(Guid id)
    {
        Id = id;
    }

    protected Entity()
    {
    }

    public Guid Id { get; init; }

    public List<IDomainEvent> PopDomainEvents()
    {
        var copy = _domainEvents.ToList();

        _domainEvents.Clear();

        return copy;
    }
}