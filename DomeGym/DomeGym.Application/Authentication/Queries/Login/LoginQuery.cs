using DomeGym.Application.Authentication.Common;
using ErrorOr;
using MediatR;

namespace DomeGym.Application.Authentication.Queries.Login;

public record LoginQuery(
    string Email,
    string Password) : IRequest<ErrorOr<AuthenticationResult>>;