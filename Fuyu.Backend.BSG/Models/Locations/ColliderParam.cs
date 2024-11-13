using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.Models.Locations
{
    [DataContract]
    public class ColliderParam
    {
        [DataMember]
        public string _parent;

        [DataMember]
        public ColliderProperties _props;
    }
}