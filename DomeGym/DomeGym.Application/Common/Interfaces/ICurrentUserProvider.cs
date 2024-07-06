using DomeGym.Application.Common.Models;

namespace DomeGym.Application.Common.Interfaces;

public interface ICurrentUserProvider
{
    CurrentUser GetCurrentUser();
}