using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.ItemTemplates
{
    [DataContract]
    public class LockableContainerItemProperties : CompoundItemItemProperties
    {
        [DataMember(Name = "KeyIds")]
        public string[] KeyIds { get; set; }
    }
}
