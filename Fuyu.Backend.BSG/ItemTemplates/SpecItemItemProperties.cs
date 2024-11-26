using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.ItemTemplates
{
    [DataContract]
    public class SpecItemItemProperties : ItemProperties
    {
        [DataMember(Name = "apResource")]
        public int apResource;

        [DataMember(Name = "krResource")]
        public int krResource;
    }
}
