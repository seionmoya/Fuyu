using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.Models.Profiles.Stats
{
    [DataContract]
    public class BodyParts
    {
        // TODO: proper type
        [DataMember]
        public object[] Head { get; set; }

        // TODO: proper type
        [DataMember]
        public object[] Chest { get; set; }

        // TODO: proper type
        [DataMember]
        public object[] Stomach { get; set; }

        // TODO: proper type
        [DataMember]
        public object[] LeftArm { get; set; }

        // TODO: proper type
        [DataMember]
        public object[] RightArm { get; set; }

        // TODO: proper type
        [DataMember]
        public object[] LeftLeg { get; set; }

        // TODO: proper type
        [DataMember]
        public object[] RightLeg { get; set; }
    }
}