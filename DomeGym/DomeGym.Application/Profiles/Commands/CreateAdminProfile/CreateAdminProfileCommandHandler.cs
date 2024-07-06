using DomeGym.Application.Common.Interfaces;
using DomeGym.Domain.Admins;
using ErrorOr;
using MediatR;

namespace DomeGym.Application.Profiles.Commands.CreateAdminProfile;

public class CreateAdminProfileCommandHandler(
    IUsersRepository usersRepository,
    IAdminsRepository adminsRepository,
    IUnitOfWork unitOfWork,
    ICurrentUserProvider currentUserProvider)
    : IRequestHandler<CreateAdminProfileCommand, ErrorOr<Guid>>
{
    public async Task<ErrorOr<Guid>> Handle(CreateAdminProfileCommand command, CancellationToken cancellationToken)
    {
        var currentUser = currentUserProvider.GetCurrentUser();
        if (currentUser.Id != command.UserId) return Error.Unauthorized(description: "User is forbidden from taking this action.");

        var user = await usersRepository.GetByIdAsync(command.UserId);
        if (user is null) return Error.NotFound(description: "User not found");

        var createAdminProfileResult = user.CreateAdminProfile();
        var admin = new Admin(user.Id, id: createAdminProfileResult.Value);

        await usersRepository.UpdateAsync(user);
        await adminsRepository.AddAdminAsync(admin);
        await unitOfWork.CommitChangesAsync();

        return createAdminProfileResult;
    }
}