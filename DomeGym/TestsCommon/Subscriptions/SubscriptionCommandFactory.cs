using DomeGym.Application.Subscriptions.Commands.CreateSubscription;
using DomeGym.Domain.Subscriptions;
using TestsCommon.TestConstants;

namespace TestsCommon.Subscriptions;

public static class SubscriptionCommandFactory
{
    public static CreateSubscriptionCommand CreateCreateSubscriptionCommand(
        SubscriptionType? subscriptionType = null,
        Guid? adminId = null)
    {
        return new CreateSubscriptionCommand(
            SubscriptionType: subscriptionType ?? Constants.Subscriptions.DefaultSubscriptionType,
            AdminId: adminId ?? Constants.Admin.Id);
    }
}