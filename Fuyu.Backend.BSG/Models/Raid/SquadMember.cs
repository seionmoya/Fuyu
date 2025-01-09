using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.Models.Raid;

[DataContract]
public class SquadMember
{
    [DataMember]
    public string _id { get; set; }

    [DataMember]
    public int aid { get; set; }

    [DataMember]
    public SquadInfo Info { get; set; }

    [DataMember]
    public bool isLeader { get; set; }

    [DataMember]
    public bool isReady { get; set; }

    // TODO: proper type
    [DataMember]
    public object PlayerVisualRepresentation { get; set; }
}