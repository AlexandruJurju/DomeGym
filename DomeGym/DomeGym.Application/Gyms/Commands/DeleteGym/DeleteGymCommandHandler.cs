using DomeGym.Application.Common.Interfaces;
using ErrorOr;
using MediatR;

namespace DomeGym.Application.Gyms.Commands.DeleteGym;

public class DeleteGymCommandHandler(
    ISubscriptionsRepository subscriptionsRepository,
    IGymsRepository gymsRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteGymCommand, ErrorOr<Deleted>>
{
    private readonly IGymsRepository _gymsRepository = gymsRepository;
    private readonly ISubscriptionsRepository _subscriptionsRepository = subscriptionsRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<ErrorOr<Deleted>> Handle(DeleteGymCommand command, CancellationToken cancellationToken)
    {
        var gym = await _gymsRepository.GetByIdAsync(command.GymId);

        if (gym is null) return Error.NotFound(description: "Gym not found");

        var subscription = await _subscriptionsRepository.GetByIdAsync(command.SubscriptionId);

        if (subscription is null) return Error.NotFound(description: "Subscription not found");

        if (!subscription.HasGym(command.GymId)) return Error.Unexpected(description: "Gym not found");

        subscription.RemoveGym(command.GymId);

        await _subscriptionsRepository.UpdateAsync(subscription);
        await _gymsRepository.RemoveGymAsync(gym);
        await _unitOfWork.CommitChangesAsync();

        return Result.Deleted;
    }
}