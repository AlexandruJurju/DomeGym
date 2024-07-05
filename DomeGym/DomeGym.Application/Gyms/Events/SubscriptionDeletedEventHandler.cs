using DomeGym.Application.Common.Interfaces;
using DomeGym.Domain.Admins.Events;
using MediatR;

namespace DomeGym.Application.Gyms.Events;

public class SubscriptionDeletedEventHandler : INotificationHandler<SubscriptionDeletedEvent>
{
    private readonly IGymsRepository _gymsRepository;
    private readonly IUnitOfWork _unitOfWork;

    public SubscriptionDeletedEventHandler(
        IGymsRepository gymsRepository,
        IUnitOfWork unitOfWork)
    {
        _gymsRepository = gymsRepository;
        _unitOfWork = unitOfWork;
    }


    public async Task Handle(SubscriptionDeletedEvent notification, CancellationToken cancellationToken)
    {
        var gyms = await _gymsRepository.ListBySubscriptionIdAsync(notification.SubscriptionId);
        await _gymsRepository.RemoveRangeAsync(gyms);
        await _unitOfWork.CommitChangesAsync();
    }
}