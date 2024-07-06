using DomeGym.Application.Common.Interfaces;
using DomeGym.Domain.Subscriptions;
using ErrorOr;
using MediatR;

namespace DomeGym.Application.Subscriptions.Commands.CreateSubscription;

public class CreateSubscriptionCommandHandler(
    ISubscriptionsRepository subscriptionsRepository,
    IUnitOfWork unitOfWork,
    IAdminsRepository adminsRepository)
    : IRequestHandler<CreateSubscriptionCommand, ErrorOr<Subscription>>
{
    private readonly IAdminsRepository _adminsRepository = adminsRepository;
    private readonly ISubscriptionsRepository _subscriptionsRepository = subscriptionsRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<ErrorOr<Subscription>> Handle(CreateSubscriptionCommand request, CancellationToken cancellationToken)
    {
        var admin = await _adminsRepository.GetByIdAsync(request.AdminId);

        if (admin is null) return Error.NotFound(description: "Admin not found");

        var subscription = new Subscription(
            request.SubscriptionType,
            request.AdminId);

        if (admin.SubscriptionId is not null) return Error.Conflict(description: "Admin already has an active subscription");

        admin.SetSubscription(subscription);

        await _subscriptionsRepository.AddSubscriptionAsync(subscription);
        await _adminsRepository.UpdateAsync(admin);
        await _unitOfWork.CommitChangesAsync();

        return subscription;
    }
}