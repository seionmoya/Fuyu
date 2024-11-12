using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.Models.Items
{
    [DataContract]
    public class ItemLockableComponent
    {
        [DataMember]
        public bool Locked;
    }
}