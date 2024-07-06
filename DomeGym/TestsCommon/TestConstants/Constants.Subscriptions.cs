﻿using DomeGym.Domain.Subscriptions;

namespace TestsCommon.TestConstants;

public static partial class Constants
{
    public static class Subscriptions
    {
        public const int MaxSessionsFreeTier = 3;
        public const int MaxRoomsFreeTier = 1;
        public const int MaxGymsFreeTier = 1;
        public static readonly SubscriptionType DefaultSubscriptionType = SubscriptionType.Free;
        public static readonly Guid Id = Guid.NewGuid();
    }
}