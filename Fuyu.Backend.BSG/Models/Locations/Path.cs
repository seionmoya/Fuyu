using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.Models.Locations
{
    [DataContract]
    public class Path
    {
        [DataMember]
        public string Source { get; set; }

        [DataMember]
        public string Destination { get; set; }
    }
}