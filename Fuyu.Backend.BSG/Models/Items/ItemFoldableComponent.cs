using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.Models.Items
{
    [DataContract]
    public class ItemFoldableComponent
    {
        [DataMember]
        public bool Folded;
    }
}