using ErrorOr;

namespace DomeGym.Domain.Subscriptions;

public static class SubscriptionErrors
{
    public static readonly Error CannotHaveMoreGymsThanTheSubscriptionAllows = Error.Validation(
        "Subscription.CannotHaveMoreGymsThanTheSubscriptionAllows",
        "A subscription cannot have more gyms than the subscription allows");
}