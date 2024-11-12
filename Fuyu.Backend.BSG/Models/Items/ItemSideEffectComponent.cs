using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.Models.Items
{
    [DataContract]
    public class ItemSideEffectComponent
    {
        [DataMember]
        public float Value;
    }
}