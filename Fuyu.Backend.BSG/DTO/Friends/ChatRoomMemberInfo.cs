using System;
using System.Runtime.Serialization;
using Fuyu.Backend.BSG.DTO.Profiles.Info;

namespace Fuyu.Backend.BSG.DTO.Friends
{
    [DataContract]
    public class ChatRoomMemberInfo
    {
        [DataMember(Name = "Nickname")]
        public string Nickname { get; set; }

        [DataMember(Name = "Side")]
        public EChatMemberSide Side { get; set; }

        [DataMember(Name = "Level")]
        public int Level { get; set; }

        [DataMember(Name = "MemberCategory")]
        public EMemberCategory MemberCategory { get; set; }

        [DataMember(Name = "SelectedMemberCategory")]
        public EMemberCategory SelectedMemberCategory { get; set; }

        [DataMember(Name = "Ignored", EmitDefaultValue = false)]
        public bool Ignored { get; set; }

        [DataMember(Name = "Banned", EmitDefaultValue = false)]
        public bool Banned { get; set; }

        [DataMember(Name = "GlobalMuteTime", EmitDefaultValue = false)]
        public DateTime GlobalMuteTime { get; set; }
    }
}