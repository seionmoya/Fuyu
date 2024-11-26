using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.ItemTemplates
{
    [DataContract]
    public class CultistAmuletItemProperties : SpecItemItemProperties
    {
        [DataMember(Name = "MaxUsages")]
        public int MaxUsages { get; set; }
    }
}
