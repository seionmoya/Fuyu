using System.Runtime.Serialization;
using Fuyu.Backend.BSG.Models.Common;

namespace Fuyu.Backend.BSG.Models.Locations;

[DataContract]
public class Location
{
    [DataMember]
    public bool Enabled { get; set; }

    [DataMember]
    public bool EnableCoop { get; set; }

    [DataMember]
    public bool ForceOnlineRaidInPVE { get; set; }

    [DataMember]
    public bool Locked { get; set; }

    [DataMember]
    public string Name { get; set; }

    [DataMember]
    public ResourceKey Scene { get; set; }

    [DataMember]
    public float Area { get; set; }

    [DataMember]
    public int RequiredPlayerLevelMin { get; set; }

    [DataMember]
    public int RequiredPlayerLevelMax { get; set; }

    [DataMember]
    public int PmcMaxPlayersInGroup { get; set; }

    [DataMember]
    public int ScavMaxPlayersInGroup { get; set; }

    [DataMember]
    public int MinPlayers { get; set; }

    [DataMember]
    public int MaxPlayers { get; set; }

    [DataMember]
    public int MaxCoopGroup { get; set; }

    [DataMember]
    public int IconX { get; set; }

    [DataMember]
    public int IconY { get; set; }

    [DataMember]
    public Wave[] waves { get; set; }

    [DataMember]
    public ItemLimit[] limits { get; set; }

    [DataMember]
    public int AveragePlayTime { get; set; }

    [DataMember]
    public int AveragePlayerLevel { get; set; }

    [DataMember]
    public int EscapeTimeLimit { get; set; }

    [DataMember]
    public int EscapeTimeLimitPVE { get; set; }

    [DataMember]
    public int EscapeTimeLimitCoop { get; set; }

    [DataMember]
    public string Rules { get; set; }

    [DataMember]
    public bool IsSecret { get; set; }

    [DataMember]
    public int MaxDistToFreePoint { get; set; }

    [DataMember]
    public int MinDistToFreePoint { get; set; }

    [DataMember]
    public int MaxBotPerZone { get; set; }

    [DataMember]
    public string OpenZones { get; set; }

    [DataMember]
    public bool OcculsionCullingEnabled { get; set; }

    [DataMember]
    public bool OldSpawn { get; set; }

    [DataMember]
    public bool OfflineOldSpawn { get; set; }

    [DataMember]
    public bool NewSpawn { get; set; }

    [DataMember]
    public bool OfflineNewSpawn { get; set; }

    [DataMember]
    public int BotMax { get; set; }

    [DataMember]
    public int BotMaxPvE { get; set; }

    [DataMember]
    public int BotStart { get; set; }

    [DataMember]
    public int BotStartPlayer { get; set; }

    [DataMember]
    public int BotStop { get; set; }

    [DataMember]
    public int BotSpawnTimeOnMin { get; set; }

    [DataMember]
    public int BotSpawnTimeOnMax { get; set; }

    [DataMember]
    public int BotSpawnTimeOffMin { get; set; }

    [DataMember]
    public int BotSpawnTimeOffMax { get; set; }

    [DataMember]
    public int BotEasy { get; set; }

    [DataMember]
    public int BotNormal { get; set; }

    [DataMember]
    public int BotHard { get; set; }

    [DataMember]
    public int BotImpossible { get; set; }

    [DataMember]
    public int BotAssault { get; set; }

    [DataMember]
    public int BotMarksman { get; set; }

    [DataMember]
    public string DisabledScavExits { get; set; }

    [DataMember]
    public int MinPlayerLvlAccessKeys { get; set; }

    [DataMember]
    public string[] AccessKeys { get; set; }

    [DataMember]
    public long UnixDateTime { get; set; }

    [DataMember]
    public GroupScenario NonWaveGroupScenario { get; set; }

    [DataMember]
    public int BotSpawnCountStep { get; set; }

    [DataMember]
    public int BotSpawnPeriodCheck { get; set; }

    [DataMember]
    public int GlobalContainerChanceModifier { get; set; }

    [DataMember]
    public MinMaxBot[] MinMaxBots { get; set; }

    [DataMember]
    public BotLocationModifier BotLocationModifier { get; set; }

    [DataMember]
    public Exit[] exits { get; set; }

    [DataMember]
    public bool DisabledForScav { get; set; }

    [DataMember]
    public MaxItemCount[] maxItemCountInLocation { get; set; }

    [DataMember]
    public BossSpawn[] BossLocationSpawn { get; set; }

    [DataMember]
    public SpawnPointParam[] spawnPointParams { get; set; }

    [DataMember(EmitDefaultValue = false)]
    public AirdropParameters[] AirdropParameters { get; set; }

    [DataMember(EmitDefaultValue = false)]
    public MatchMakerWaitTime[] MatchMakerMinPlayersByWaitTime { get; set; }

    [DataMember]
    public Transit[] transits { get; set; }

    [DataMember]
    public string Id { get; set; }

    [DataMember]
    public string _Id { get; set; }

    [DataMember]
    public LootItem[] Loot { get; set; }

    [DataMember]
    public Banner[] Banners { get; set; }

    [DataMember]
    public Events Events { get; set; }
}