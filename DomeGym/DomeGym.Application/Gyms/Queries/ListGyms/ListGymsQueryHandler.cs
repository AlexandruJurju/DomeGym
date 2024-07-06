using DomeGym.Application.Common.Interfaces;
using DomeGym.Domain.Gyms;
using ErrorOr;
using MediatR;

namespace DomeGym.Application.Gyms.Queries.ListGyms;

public class ListGymsQueryHandler(IGymsRepository gymsRepository, ISubscriptionsRepository subscriptionsRepository)
    : IRequestHandler<ListGymsQuery, ErrorOr<List<Gym>>>
{
    private readonly IGymsRepository _gymsRepository = gymsRepository;
    private readonly ISubscriptionsRepository _subscriptionsRepository = subscriptionsRepository;

    public async Task<ErrorOr<List<Gym>>> Handle(ListGymsQuery query, CancellationToken cancellationToken)
    {
        if (!await _subscriptionsRepository.ExistsAsync(query.SubscriptionId)) return Error.NotFound(description: "Subscription not found");

        return await _gymsRepository.ListBySubscriptionIdAsync(query.SubscriptionId);
    }
}