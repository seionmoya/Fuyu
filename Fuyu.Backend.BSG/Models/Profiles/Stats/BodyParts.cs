using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.Models.Profiles.Stats
{
    [DataContract]
    public class BodyParts
    {
        // TODO: proper type
        [DataMember]
        public object[] Head;

        // TODO: proper type
        [DataMember]
        public object[] Chest;

        // TODO: proper type
        [DataMember]
        public object[] Stomach;

        // TODO: proper type
        [DataMember]
        public object[] LeftArm;

        // TODO: proper type
        [DataMember]
        public object[] RightArm;

        // TODO: proper type
        [DataMember]
        public object[] LeftLeg;

        // TODO: proper type
        [DataMember]
        public object[] RightLeg;
    }
}