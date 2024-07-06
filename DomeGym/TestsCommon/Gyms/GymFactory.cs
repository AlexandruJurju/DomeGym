using DomeGym.Domain.Gyms;
using TestsCommon.TestConstants;

namespace TestsCommon.Gyms;

public static class GymFactory
{
    public static Gym CreateGym(
        string name = Constants.Gym.Name,
        int maxRooms = Constants.Subscriptions.MaxRoomsFreeTier,
        Guid? id = null)
    {
        return new Gym(
            name,
            maxRooms,
            Constants.Subscriptions.Id,
            id ?? Constants.Gym.Id);
    }
}