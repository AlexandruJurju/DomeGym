using DomeGym.Application.Common.Interfaces;
using DomeGym.Domain.Gyms;
using ErrorOr;
using MediatR;

namespace DomeGym.Application.Gyms.Commands.CreateGym;

public class CreateGymCommandHandler(
    ISubscriptionsRepository subscriptionsRepository,
    IGymsRepository gymsRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<CreateGymCommand, ErrorOr<Gym>>
{
    private readonly IGymsRepository _gymsRepository = gymsRepository;
    private readonly ISubscriptionsRepository _subscriptionsRepository = subscriptionsRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<ErrorOr<Gym>> Handle(CreateGymCommand command, CancellationToken cancellationToken)
    {
        var subscription = await _subscriptionsRepository.GetByIdAsync(command.SubscriptionId);

        if (subscription is null) return Error.NotFound(description: "Subscription not found");

        var gym = new Gym(
            command.Name,
            subscription.GetMaxRooms(),
            subscription.Id);

        var addGymResult = subscription.AddGym(gym);

        if (addGymResult.IsError) return addGymResult.Errors;

        await _subscriptionsRepository.UpdateAsync(subscription);
        await _gymsRepository.AddGymAsync(gym);
        await _unitOfWork.CommitChangesAsync();

        return gym;
    }
}