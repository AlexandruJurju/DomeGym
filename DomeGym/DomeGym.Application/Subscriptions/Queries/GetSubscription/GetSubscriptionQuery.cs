using DomeGym.Domain.Subscriptions;
using ErrorOr;
using MediatR;

namespace DomeGym.Application.Subscriptions.Queries.GetSubscription;

public record GetSubscriptionQuery(Guid SubscriptionId)
    : IRequest<ErrorOr<Subscription>>;