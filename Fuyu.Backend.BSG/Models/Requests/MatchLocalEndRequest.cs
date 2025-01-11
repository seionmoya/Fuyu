using System.Collections.Generic;
using System.Runtime.Serialization;
using Fuyu.Backend.BSG.Models.Items;
using Fuyu.Backend.BSG.Models.Raid;

namespace Fuyu.Backend.BSG.Models.Requests;

[DataContract]
public class MatchLocalEndRequest
{
    [DataMember(Name = "profile")]
    public string ServerId { get; set; }

    [DataMember(Name = "results")]
    public MatchLocalEndResult Results { get; set; }

    [DataMember(Name = "lostInsuredItems")]
    public ItemInstance[] LostInsuredItems { get; set; }

    [DataMember(Name = "transferItems")]
    public Dictionary<string, ItemInstance[]> TransferItems { get; set; }

    // TODO: proper type
    [DataMember(Name = "locationTransit")]
    public TransitData LocationTransit { get; set; }
}