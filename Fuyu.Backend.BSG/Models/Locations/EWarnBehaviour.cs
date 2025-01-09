using System;

namespace Fuyu.Backend.BSG.Models.Locations;

[Flags]
public enum EWarnBehaviour
{
    Default = 1,
    Neutral = 2,
    Warn = 4,
    AlwaysEnemies = 8,
    AlwaysFriends = 16,
    ChancedEnemies = 32
}