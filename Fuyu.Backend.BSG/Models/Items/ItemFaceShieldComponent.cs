using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.Models.Items
{
    [DataContract]
    public class ItemFaceShieldComponent
    {
        [DataMember]
        public byte Hits { get; set; }

        [DataMember]
        public byte HitSeed { get; set; }
    }
}