
using DomeGym.Domain.Subscriptions;
using TestsCommon.TestConstants;

namespace TestsCommon.Subscriptions;

public static class SubscriptionFactory
{
    public static Subscription CreateSubscription(
        SubscriptionType? subscriptionType = null,
        Guid? adminId = null,
        Guid? id = null)
    {
        return new Subscription(
            subscriptionType: subscriptionType ?? Constants.Subscriptions.DefaultSubscriptionType,
            adminId: adminId ?? TestConstants.Constants.Admin.Id,
            id: id ?? TestConstants.Constants.Subscriptions.Id);
    }
}