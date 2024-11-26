using System.Runtime.Serialization;
using Fuyu.Backend.BSG.Models.Profiles.Skills;

namespace Fuyu.Backend.BSG.Models.Profiles
{
    [DataContract]
    public class SkillInfo
    {
        [DataMember]
        public Skill[] Common;

        [DataMember]
        public Mastery[] Mastering;

        [DataMember]
        public int Points;
    }
}