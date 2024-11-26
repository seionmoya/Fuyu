using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.ItemTemplates
{
    [DataContract]
    public class MapItemProperties : ItemProperties
    {
        [DataMember(Name = "ConfigPathStr")]
        public string ConfigPathStr;

        [DataMember(Name = "MaxMarkersCount")]
        public int MaxMarkersCount;

        [DataMember(Name = "scaleMin")]
        public float scaleMin;

        [DataMember(Name = "scaleMax")]
        public float scaleMax;
    }
}
