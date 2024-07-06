using DomeGym.Domain.Common;

namespace DomeGym.Domain.Admins.Events;

public record SubscriptionDeletedEvent(Guid SubscriptionId) : IDomainEvent;