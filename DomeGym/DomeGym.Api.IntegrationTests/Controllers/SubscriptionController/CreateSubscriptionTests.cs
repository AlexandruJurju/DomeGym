using System.Net;
using System.Net.Http.Json;
using DomeGym.Api.IntegrationTests.Common;
using DomeGym.Contracts.Subscriptions;
using FluentAssertions;
using TestsCommon.TestConstants;

namespace DomeGym.Api.IntegrationTests.Controllers.SubscriptionController;

[Collection(GymManagementApiFactoryCollection.CollectionName)]
public class CreateSubscriptionTests
{
    private readonly HttpClient _httpClient;

    public CreateSubscriptionTests(GymManagementApiFactory apiFactory)
    {
        _httpClient = apiFactory.HttpClient;
        apiFactory.ResetDatabase();
    }

    [Theory]
    [MemberData(nameof(ListSubscriptionTypes))]
    public async Task CreateSubscription_WhenValidSubscription_ShouldCreateSubscription(SubscriptionType subscriptionType)
    {
        // Arrange
        var createSubscriptionRequest = new CreateSubscriptionRequest(
            SubscriptionType: subscriptionType,
            Constants.Admin.Id);

        // Act
        var response = await _httpClient.PostAsJsonAsync("Subscriptions", createSubscriptionRequest);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        response.Headers.Location.Should().NotBeNull();

        var subscriptionResponse = await response.Content.ReadFromJsonAsync<SubscriptionResponse>();
        subscriptionResponse!.Should().NotBeNull();
        subscriptionResponse!.SubscriptionType.Should().Be(subscriptionType);
    }


    public static TheoryData<SubscriptionType> ListSubscriptionTypes()
    {
        var subscriptionTypes = Enum.GetValues<SubscriptionType>().ToList();

        var theoryData = new TheoryData<SubscriptionType>();

        foreach (var subscriptionType in subscriptionTypes)
        {
            theoryData.Add(subscriptionType);
        }

        return theoryData;
    }
}