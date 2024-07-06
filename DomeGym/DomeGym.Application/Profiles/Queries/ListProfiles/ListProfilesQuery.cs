using ErrorOr;
using MediatR;

namespace DomeGym.Application.Profiles.Queries.ListProfiles;

public record ListProfilesQuery(Guid UserId) : IRequest<ErrorOr<ListProfilesResult>>;