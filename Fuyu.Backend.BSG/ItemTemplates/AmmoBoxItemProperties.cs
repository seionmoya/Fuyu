using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.ItemTemplates
{
    [DataContract]
    public class AmmoBoxItemProperties : StackableItemItemProperties
    {
        [DataMember(Name = "magAnimationIndex")]
        public int magAnimationIndex;

        [DataMember(Name = "StackSlots")]
        public object[] StackSlots = [];
    }
}
