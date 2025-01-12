using System.Runtime.Serialization;
using Fuyu.Backend.BSG.Models.Bots;

namespace Fuyu.Backend.BSG.Models.Locations;

[DataContract]
public class AdditionalHostilitySettings
{
    [DataMember]
    public EWildSpawnType BotRole { get; set; }

    [DataMember]
    public EWildSpawnType[] AlwaysEnemies { get; set; }

    [DataMember]
    public ChancedEnemy[] ChancedEnemies { get; set; }

    [DataMember]
    public EWildSpawnType[] Warn { get; set; }

    [DataMember]
    public EWildSpawnType[] Neutral { get; set; }

    [DataMember]
    public EWildSpawnType[] AlwaysFriends { get; set; }

    [DataMember]
    public EWarnBehaviour SavagePlayerBehaviour { get; set; }

    [DataMember]
    public int SavageEnemyChance { get; set; }

    [DataMember]
    public EWarnBehaviour BearPlayerBehaviour { get; set; }

    [DataMember]
    public int BearEnemyChance { get; set; }

    [DataMember]
    public EWarnBehaviour UsecPlayerBehaviour { get; set; }

    [DataMember]
    public int UsecEnemyChance { get; set; }
}