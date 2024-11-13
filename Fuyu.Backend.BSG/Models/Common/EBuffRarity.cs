using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.Models.Common
{
    public enum EBuffRarity
    {
        [DataMember(Name = "common")]
        Common,
        [DataMember(Name = "rare")]
        Rare
    }
}
