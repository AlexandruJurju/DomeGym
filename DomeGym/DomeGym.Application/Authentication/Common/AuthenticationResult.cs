using DomeGym.Domain.Users;

namespace DomeGym.Application.Authentication.Common;

public record AuthenticationResult(
    User User,
    string Token);