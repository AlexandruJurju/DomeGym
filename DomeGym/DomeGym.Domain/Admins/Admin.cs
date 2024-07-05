using DomeGym.Domain.Admins.Events;
using DomeGym.Domain.Common;
using DomeGym.Domain.Subscriptions;
using Throw;

namespace DomeGym.Domain.Admins;

public class Admin : Entity
{
    public Admin(
        Guid userId,
        Guid? subscriptionId = null,
        Guid? id = null) : base(id ?? Guid.NewGuid())
    {
        UserId = userId;
        SubscriptionId = subscriptionId;
    }
    
    public Guid UserId { get; }
    public Guid? SubscriptionId { get; private set; }

    public void SetSubscription(Subscription subscription)
    {
        SubscriptionId.HasValue.Throw().IfTrue();

        SubscriptionId = subscription.Id;
    }

    public void DeleteSubscription(Guid subscriptionId)
    {
        SubscriptionId.ThrowIfNull().IfNotEquals(subscriptionId);

        SubscriptionId = null;

        _domainEvents.Add(new SubscriptionDeletedEvent(subscriptionId));
    }

    // for EF Core
    private Admin()
    {
    }
}