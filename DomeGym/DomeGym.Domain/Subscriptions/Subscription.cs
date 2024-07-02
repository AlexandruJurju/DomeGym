namespace DomeGym.Domain.Subscriptions;

public class Subscription
{
    private readonly Guid _adminId;

    public Subscription(
        SubscriptionType subscriptionType,
        Guid adminId,
        Guid? id = null
    )
    {
        Id = id ?? Guid.NewGuid();
        _adminId = adminId;
        SubscriptionType = subscriptionType;
    }

    private Subscription()
    {
    }

    public Guid Id { get; private set; }
    public SubscriptionType SubscriptionType { get; private set; } = null!;
}