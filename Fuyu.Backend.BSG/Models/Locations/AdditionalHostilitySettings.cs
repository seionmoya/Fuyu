using System.Runtime.Serialization;
using Fuyu.Backend.BSG.Models.Bots;

namespace Fuyu.Backend.BSG.Models.Locations;

[DataContract]
public class AdditionalHostilitySettings
{
    [DataMember]
    public EBotRole BotRole { get; set; }

    [DataMember]
    public EBotRole[] AlwaysEnemies { get; set; }

    [DataMember]
    public ChancedEnemy[] ChancedEnemies { get; set; }

    [DataMember]
    public EBotRole[] Warn { get; set; }

    [DataMember]
    public EBotRole[] Neutral { get; set; }

    [DataMember]
    public EBotRole[] AlwaysFriends { get; set; }

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
