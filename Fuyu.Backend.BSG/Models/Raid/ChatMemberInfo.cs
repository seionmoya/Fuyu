using System.Runtime.Serialization;
using Fuyu.Backend.BSG.Models.Profiles.Info;

namespace Fuyu.Backend.BSG.Models.Raid;

[DataContract]
public class ChatMemberInfo
{
    [DataMember]
    public string Nickname { get; set; }

    [DataMember]
    public EPlayerSide Side { get; set; }

    [DataMember]
    public int Level { get; set; }

    [DataMember]
    public EMemberCategory MemberCategory { get; set; }

    [DataMember]
    public EMemberCategory SelectedCategory { get; set; }

    [DataMember]
    public string GameVersion { get; set; }

    [DataMember]
    public int SavageLockTime { get; set; }

    [DataMember]
    public string SavageNickname { get; set; }

    [DataMember]
    public bool HasCoopExtension { get; set; }
}