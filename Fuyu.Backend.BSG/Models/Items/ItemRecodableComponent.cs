using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.Models.Items
{
    [DataContract]
    public class ItemRecodableComponent
    {
        [DataMember]
        public bool IsEncoded;
    }
}