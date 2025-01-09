using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.Models.Profiles.Stats;

[DataContract]
public class DamageHistory
{
    [DataMember]
    public string LethalDamagePart { get; set; }

    // TODO: proper type
    [DataMember]
    public object LethalDamage { get; set; }

    // TODO: proper type
    [DataMember]
    public object[] BodyParts { get; set; }
}