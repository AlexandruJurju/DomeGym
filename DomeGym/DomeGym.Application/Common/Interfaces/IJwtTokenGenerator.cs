using DomeGym.Domain.Users;

namespace DomeGym.Application.Common.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}