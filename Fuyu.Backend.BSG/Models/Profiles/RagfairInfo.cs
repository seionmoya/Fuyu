using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.Models.Profiles
{
    [DataContract]
    public class RagfairInfo
    {
        [DataMember]
        public float rating { get; set; }

        [DataMember]
        public bool isRatingGrowing { get; set; }

        // TODO: proper type
        [DataMember]
        public object[] offers { get; set; }
    }
}