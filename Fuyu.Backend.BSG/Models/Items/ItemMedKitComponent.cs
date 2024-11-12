using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.Models.Items
{
    [DataContract]
    public class ItemMedKitComponent
    {
        [DataMember]
        public float HpResource;
    }
}