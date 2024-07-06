using ErrorOr;

namespace DomeGym.Application.Authentication.Common;

public static class AuthenticationErrors
{
    public static readonly Error InvalidCredentials = Error.Validation(
        "Authentication.InvalidCredentials",
        "Invalid credentials");
}