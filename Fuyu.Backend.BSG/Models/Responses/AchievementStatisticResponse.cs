using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.Models.Responses;

[DataContract]
public class AchievementStatisticResponse
{
    [DataMember]
    public Dictionary<string, float> elements { get; set; }
}