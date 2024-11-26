using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.ItemTemplates
{
    [DataContract]
    public class BarterItemItemProperties : ItemProperties
    {
        [DataMember(Name = "MaxResource")]
        public int MaxResource { get; set; }
    }
}
