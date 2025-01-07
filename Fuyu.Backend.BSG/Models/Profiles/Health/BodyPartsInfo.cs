using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.Models.Profiles.Health
{
    [DataContract]
    public class BodyPartInfo
    {
        [DataMember]
        public BodyPart Head { get; set; }

        [DataMember]
        public BodyPart Chest { get; set; }

        [DataMember]
        public BodyPart Stomach { get; set; }

        [DataMember]
        public BodyPart LeftArm { get; set; }

        [DataMember]
        public BodyPart RightArm { get; set; }

        [DataMember]
        public BodyPart LeftLeg { get; set; }

        [DataMember]
        public BodyPart RightLeg { get; set; }
    }
}