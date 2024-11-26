using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.ItemTemplates
{
    [DataContract]
    public class HideoutAreaContainerItemProperties : CompoundItemItemProperties
    {
        [DataMember(Name = "LayoutName")]
        public string LayoutName { get; set; }
    }
}
