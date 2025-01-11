using System.Collections.Generic;
using System.Runtime.Serialization;
using Fuyu.Backend.BSG.Models.Raid;
using Fuyu.Common.Hashing;

namespace Fuyu.Backend.BSG.Models.Requests;

[DataContract]
public class TransitData
{
    [DataMember(Name = "hash")]
    public string Hash { get; set; }

    [DataMember(Name = "playersCount")]
    public int PlayersCount { get; set; }

    [DataMember(Name = "ip")]
    public string Ip { get; set; }

    [DataMember(Name = "location")]
    public string Location { get; set; }

    [DataMember(Name = "profiles")]
    public Dictionary<MongoId, ProfileKey> Profiles { get; set; }

    [DataMember(Name = "transitionRaidId")]
    public string TransitionRaidId { get; set; }

    [DataMember(Name = "raidMode")]
    public ERaidMode RaidMode { get; set; }

    [DataMember(Name = "side")]
    public ESideType SideType { get; set; }

    [DataMember(Name = "dayTime")]
    public EDateTime DayTime { get; set; }

    public override string ToString()
    {
        return $"TRANSITDATA: Hash: {Hash}, PlayersCount: {PlayersCount}, Ip: {Ip}, Location: {Location}, Profiles: {Profiles}x, TransitionRaidId: {TransitionRaidId}, RaidMode: {RaidMode}, SideType: {SideType}, DayTime: {DayTime}";
    }
}