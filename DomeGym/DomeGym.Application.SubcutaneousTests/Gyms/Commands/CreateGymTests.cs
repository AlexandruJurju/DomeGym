using DomeGym.Application.SubcutaneousTests.Common;
using DomeGym.Domain.Subscriptions;
using FluentAssertions;
using MediatR;
using TestsCommon.Gyms;
using TestsCommon.Subscriptions;

namespace DomeGym.Application.SubcutaneousTests.Gyms.Commands;

[Collection(MediatorFactoryCollection.CollectionName)]
public class CreateGymTests(MediatorFactory mediatorFactory)
{
    private readonly IMediator _mediator = mediatorFactory.CreateMediator();

    [Fact]
    public async Task CreateGym_WhenValidCommand_ShouldCreateGym()
    {
        // Arrange
        // Create a subscription
        var subscription = await CreateSubscription();

        // Create a valid CreateGymCommand
        var createGymCommand = GymCommandFactory.CreateCreateGymCommand(subscriptionId: subscription.Id);

        // Act
        // Send the CreateGymCommand to MediatR
        var createGymResult = await _mediator.Send(createGymCommand);

        // Assert
        // the result is a gym corresponding to the details of the creation command
        createGymResult.IsError.Should().BeFalse();
        createGymResult.Value.SubscriptionId.Should().Be(subscription.Id);
    }

    private async Task<Subscription> CreateSubscription()
    {
        // 1. Create a CreateSubscriptionCommand
        var createSubscriptionCommand = SubscriptionCommandFactory.CreateCreateSubscriptionCommand();

        // 2. Sending it to the MediatR
        var result = await _mediator.Send(createSubscriptionCommand);

        // 3. Make sure it was created successfully 
        result.IsError.Should().BeFalse();
        return result.Value;
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(200)]
    public async Task CreateGym_WhenCommandContainsInvalidData_ShouldReturnValidationError(int gymNameLength)
    {
        // Arrange
        string gymName = new('a', gymNameLength);
        var createGymCommand = GymCommandFactory.CreateCreateGymCommand(gymName);

        // Act
        var result = await _mediator.Send(createGymCommand);

        // Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Code.Should().Be("Name");
    }
}