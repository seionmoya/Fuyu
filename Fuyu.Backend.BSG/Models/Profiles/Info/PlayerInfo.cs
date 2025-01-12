using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.Models.Profiles.Info;

[DataContract]
public class PlayerInfo
{
    [DataMember(Name = "nickname")]
    public string Nickname;

    [DataMember(Name = "side")]
    public string Side;

    [DataMember(Name = "experience")]
    public int Experience;

    [DataMember(Name = "prestigeLevel")]
    public int PrestigeLevel;

    [DataMember(Name = "memberCategory")]
    public EMemberCategory MemberCategory;

    [DataMember(Name = "selectedMemberCategory")]
    public EMemberCategory SelectedMemberCategory;

    [DataMember(Name = "registrationDate")]
    public int RegistrationDate;
}