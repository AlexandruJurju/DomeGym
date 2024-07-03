using DomeGym.Domain.Gyms;
using ErrorOr;
using Throw;

namespace DomeGym.Domain.Subscriptions;

public class Subscription
{
    public Guid Id { get; private set; }
    private readonly int _maxGyms;
    public SubscriptionType SubscriptionType { get; private set; } = null!;

    public Guid AdminId { get; private set; }
    private readonly List<Guid> _gymIds = new();

    public Subscription(
        SubscriptionType subscriptionType,
        Guid adminId,
        Guid? id = null
    )
    {
        Id = id ?? Guid.NewGuid();
        AdminId = adminId;
        SubscriptionType = subscriptionType;
        _maxGyms = GetMaxGyms();
    }

    public ErrorOr<Success> AddGym(Gym gym)
    {
        _gymIds.Throw().IfContains(gym.Id);

        if (_gymIds.Count >= _maxGyms)
        {
            return SubscriptionErrors.CannotHaveMoreGymsThanSubscriptionAllows;
        }

        _gymIds.Add(gym.Id);

        return Result.Success;
    }

    public int GetMaxGyms() => SubscriptionType.Name switch
    {
        nameof(SubscriptionType.Free) => 1,
        nameof(SubscriptionType.Starter) => 1,
        nameof(SubscriptionType.Pro) => 3,
        _ => throw new InvalidOperationException()
    };

    public int GetMaxRooms() => SubscriptionType.Name switch
    {
        nameof(SubscriptionType.Free) => 1,
        nameof(SubscriptionType.Starter) => 3,
        nameof(SubscriptionType.Pro) => int.MaxValue,
        _ => throw new InvalidOperationException()
    };

    public int GetMaxDailySessions() => SubscriptionType.Name switch
    {
        nameof(SubscriptionType.Free) => 4,
        nameof(SubscriptionType.Starter) => int.MaxValue,
        nameof(SubscriptionType.Pro) => int.MaxValue,
        _ => throw new InvalidOperationException()
    };

    public bool HasGym(Guid gymId)
    {
        return _gymIds.Contains(gymId);
    }

    public void RemoveGym(Guid gymId)
    {
        _gymIds.Throw().IfNotContains(gymId);

        _gymIds.Remove(gymId);
    }


    private Subscription()
    {
    }
}