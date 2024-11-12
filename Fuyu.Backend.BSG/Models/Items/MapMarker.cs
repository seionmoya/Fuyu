using Fuyu.Backend.BSG.Models.Common;
using System.Runtime.Serialization;

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