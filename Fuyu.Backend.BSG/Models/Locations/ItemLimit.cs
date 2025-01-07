using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.Models.Locations
{
    [DataContract]
    public class ItemLimit
    {
        [DataMember]
        public string[] items { get; set; }

        [DataMember]
        public int min { get; set; }

        [DataMember]
        public int max { get; set; }
    }
}