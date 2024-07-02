using DomeGym.Application.Common.Interfaces;
using DomeGym.Domain.Subscriptions;
using ErrorOr;
using MediatR;

namespace DomeGym.Application.Subscriptions.Commands.CreateSubscription;

public class CreateSubscriptionCommandHandler : IRequestHandler<CreateSubscriptionCommand, ErrorOr<Subscription>>
{
    private readonly ISubscriptionsRepository _subscriptionsRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateSubscriptionCommandHandler(ISubscriptionsRepository subscriptionsRepository, IUnitOfWork unitOfWork)
    {
        _subscriptionsRepository = subscriptionsRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Subscription>> Handle(CreateSubscriptionCommand request, CancellationToken cancellationToken)
    {
        // Create a subscription
        var subscription = new Subscription(
            request.SubscriptionType,
            request.AdminId
        );

        // Add it to the database
        await _subscriptionsRepository.AddSubscriptionAsync(subscription);
        await _unitOfWork.CommitChangesAsync();

        // Return subscription
        return subscription;
    }
}