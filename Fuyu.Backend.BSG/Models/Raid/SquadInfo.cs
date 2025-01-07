using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.Models.Raid
{
    [DataContract]
    public class SquadInfo
    {
        [DataMember]
        public string Nickname { get; set; }

        [DataMember]
        public string Side { get; set; }

        [DataMember]
        public int Level { get; set; }

        [DataMember]
        public int MemberCategory { get; set; }

        [DataMember]
        public int SelectedCategory { get; set; }

        [DataMember]
        public string GameVersion { get; set; }

        [DataMember]
        public int SavageLockTime { get; set; }

        [DataMember]
        public string SavageNickname { get; set; }

        [DataMember]
        public bool hasCoopExtension { get; set; }
    }
}