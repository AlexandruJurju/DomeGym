using ErrorOr;
using MediatR;

namespace DomeGym.Application.Profiles.Commands.CreateAdminProfile;

public record CreateAdminProfileCommand(Guid UserId)
    : IRequest<ErrorOr<Guid>>;