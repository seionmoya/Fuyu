using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.Models.Items
{
    [DataContract]
    public class ItemRepairableComponent
    {
        [DataMember]
        public float Durability;

        [DataMember]
        public float MaxDurability;
    }
}