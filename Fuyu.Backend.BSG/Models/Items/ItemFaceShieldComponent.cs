using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.Models.Items
{
    [DataContract]
    public class ItemFaceShieldComponent
    {
        [DataMember]
        public byte Hits;

        [DataMember]
        public byte HitSeed;
    }
}