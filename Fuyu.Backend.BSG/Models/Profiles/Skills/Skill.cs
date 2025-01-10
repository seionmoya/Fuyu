using System.Runtime.Serialization;
using Newtonsoft.Json.Converters;

namespace Fuyu.Backend.BSG.Models.Profiles.Skills;

[DataContract]
public class Skill
{
    [DataMember]
    [Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
    public ESkillType Id { get; set; }

    [DataMember]
    public float Progress { get; set; }

    [DataMember]
    public float PointsEarnedDuringSession { get; set; }

    [DataMember]
    public long LastAccess { get; set; }
}