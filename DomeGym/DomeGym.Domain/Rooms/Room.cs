namespace DomeGym.Domain.Rooms;

public class Room
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = null!;

    public Guid GymId { get; private set; }
    public int MaxDailySessions { get; private set; }

    public Room(
        string name,
        Guid gymId,
        int maxDailySessions,
        Guid? id = null)
    {
        Id = id ?? Guid.NewGuid();
        Name = name;
        GymId = gymId;
        MaxDailySessions = maxDailySessions;
    }

    private Room()
    {
    }
}