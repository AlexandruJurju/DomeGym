using DomeGym.Application.Authentication.Common;
using DomeGym.Application.Common.Interfaces;
using DomeGym.Domain.Common.Interfaces;
using DomeGym.Domain.Users;
using ErrorOr;
using MediatR;

namespace DomeGym.Application.Authentication.Commands.Register;

public class RegisterCommandHandler(
    IJwtTokenGenerator _jwtTokenGenerator,
    IPasswordHasher _passwordHasher,
    IUsersRepository _usersRepository,
    IUnitOfWork _unitOfWork)
    : IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
{
    public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        if (await _usersRepository.ExistsByEmailAsync(command.Email)) return Error.Conflict(description: "User already exists");

        var hashPasswordResult = _passwordHasher.HashPassword(command.Password);

        if (hashPasswordResult.IsError) return hashPasswordResult.Errors;

        var user = new User(
            command.FirstName,
            command.LastName,
            command.Email,
            hashPasswordResult.Value);

        await _usersRepository.AddUserAsync(user);
        await _unitOfWork.CommitChangesAsync();

        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(user, token);
    }
}