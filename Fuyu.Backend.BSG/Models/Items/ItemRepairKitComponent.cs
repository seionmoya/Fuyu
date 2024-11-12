using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.Models.Items
{
    [DataContract]
    public class ItemRepairKitComponent
    {
        [DataMember]
        public float Resource;
    }
}