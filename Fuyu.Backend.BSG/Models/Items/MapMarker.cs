using System.Runtime.Serialization;
using Fuyu.Backend.BSG.Models.Common;

namespace Fuyu.Backend.BSG.Models.Items
{
    [DataContract]
    public class MapMarker
    {
        [DataMember]
        public EMapMarkerType Type;
        
        [DataMember]
        public int X;

        [DataMember]
        public int Y;

        [DataMember]
        public string Note;
    }
}