namespace DomeGym.Contracts.Profiles;

public record ListProfilesResponse(Guid? AdminId, Guid? ParticipantId, Guid? TrainerId);