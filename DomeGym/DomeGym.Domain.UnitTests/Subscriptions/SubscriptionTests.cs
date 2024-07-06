using DomeGym.Domain.Subscriptions;
using ErrorOr;
using FluentAssertions;
using TestsCommon.Gyms;
using TestsCommon.Subscriptions;

namespace DomeGym.Domain.UnitTests.Subscriptions;

public class SubscriptionTests
{
    [Fact]
    public void AddGym_WhenMoreThanSubscriptionAllows_ShouldFail()
    {
        // arrange
        // create subscription
        var subscription = SubscriptionFactory.CreateSubscription();

        // create the max number of gyms + 1
        var gyms = Enumerable.Range(0, subscription.GetMaxGyms() + 1)
            .Select(_ => GymFactory.CreateGym(id: Guid.NewGuid()))
            .ToList();

        // act
        // add the gyms
        var addGymResults = gyms.ConvertAll(subscription.AddGym);

        // assert
        // make sure it fails
        var allExceptLastResult = addGymResults[..^1];
        allExceptLastResult.Should().AllSatisfy(addGymResult => addGymResult.Value.Should().Be(Result.Success));

        var lastAddGymResult = addGymResults.Last();
        lastAddGymResult.IsError.Should().BeTrue();
        lastAddGymResult.FirstError.Should().Be(SubscriptionErrors.CannotHaveMoreGymsThanTheSubscriptionAllows);
    }
}