using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.Models.Profiles;

[DataContract]
public class UnlockedInfo
{
    // TODO: proper type
    [DataMember]
    public object[] unlockedProductionRecipe { get; set; }
}