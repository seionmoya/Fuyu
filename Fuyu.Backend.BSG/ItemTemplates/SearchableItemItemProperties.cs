using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.ItemTemplates
{
    [DataContract]
    public class SearchableItemItemProperties : CompoundItemItemProperties
    {
        [DataMember(Name = "SearchSound")]
        public string SearchSound;

        [DataMember(Name = "BlocksArmorVest")]
        public bool BlocksArmorVest;
    }
}
