using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.Models.Profiles.Health
{
    [DataContract]
    public class BodyPartInfo : IEnumerable<BodyPart>
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

        public IEnumerator<BodyPart> GetEnumerator()
        {
            yield return Head;
            yield return Chest;
            yield return Stomach;
            yield return LeftArm;
            yield return RightArm;
            yield return LeftLeg;
            yield return RightLeg;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new System.NotImplementedException();
        }
    }
}