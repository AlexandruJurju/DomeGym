using DomeGym.Application.Common.Authorization;
using DomeGym.Domain.Gyms;
using ErrorOr;
using MediatR;

namespace DomeGym.Application.Gyms.Commands.CreateGym;

[Authorize(Roles = "Admin")]
public record CreateGymCommand(string Name, Guid SubscriptionId) : IRequest<ErrorOr<Gym>>;