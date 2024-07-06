using DomeGym.Application.Authentication.Common;
using DomeGym.Application.Common.Interfaces;
using DomeGym.Domain.Common.Interfaces;
using ErrorOr;
using MediatR;

namespace DomeGym.Application.Authentication.Queries.Login;

public class LoginQueryHandler(
    IJwtTokenGenerator _jwtTokenGenerator,
    IPasswordHasher _passwordHasher,
    IUsersRepository _usersRepository)
    : IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
{
    public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
    {
        var user = await _usersRepository.GetByEmailAsync(query.Email);

        return user is null || !user.IsCorrectPasswordHash(query.Password, _passwordHasher)
            ? AuthenticationErrors.InvalidCredentials
            : new AuthenticationResult(user, _jwtTokenGenerator.GenerateToken(user));
    }
}