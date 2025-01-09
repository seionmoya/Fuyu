using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.ItemTemplates
{
    [DataContract]
    public class EquipmentItemProperties : CompoundItemItemProperties
    {
        [DataMember(Name = "BlocksEarpiece")]
        public bool BlocksEarpiece;

        [DataMember(Name = "BlocksEyewear")]
        public bool BlocksEyewear;

        [DataMember(Name = "BlocksFaceCover")]
        public bool BlocksFaceCover;

        [DataMember(Name = "BlocksHeadwear")]
        public bool BlocksHeadwear;
    }
}