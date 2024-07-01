using MediatR;

namespace DomeGym.Application.Subscriptions.Commands.CreateSubscription;

public record CreateSubscriptionCommand(
    string SubscriptionType,
    Guid AdminId
) : IRequest<Guid>;