using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.Models.Profiles
{
    [DataContract]
    public class ConditionCounter
    {
        [DataMember]
        public string id;

        [DataMember]
        public string sourceId;

        [DataMember]
        public string type;

        [DataMember]
        public int value;
    }
}