using DomeGym.Application.Common.Interfaces;
using ErrorOr;
using MediatR;

namespace DomeGym.Application.Gyms.Commands.AddTrainer;

public class AddTrainerCommandHandler(
    IGymsRepository gymsRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<AddTrainerCommand, ErrorOr<Success>>
{
    private readonly IGymsRepository _gymsRepository = gymsRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<ErrorOr<Success>> Handle(AddTrainerCommand command, CancellationToken cancellationToken)
    {
        var gym = await _gymsRepository.GetByIdAsync(command.GymId);

        if (gym is null) return Error.NotFound(description: "Gym not found");

        var addTrainerResult = gym.AddTrainer(command.TrainerId);

        if (addTrainerResult.IsError) return addTrainerResult.Errors;

        await _gymsRepository.UpdateGymAsync(gym);
        await _unitOfWork.CommitChangesAsync();

        return Result.Success;
    }
}