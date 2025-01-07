using System.Runtime.Serialization;
using Fuyu.Backend.BSG.Models.Common;

namespace Fuyu.Backend.BSG.Models.Items
{
    [DataContract]
    public class MapMarker
    {
        [DataMember]
        public EMapMarkerType Type { get; set; }

        [DataMember]
        public int X { get; set; }

        [DataMember]
        public int Y { get; set; }

        [DataMember]
        public string Note { get; set; }
    }
}