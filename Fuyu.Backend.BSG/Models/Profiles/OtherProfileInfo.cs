using System.Runtime.Serialization;
using Fuyu.Backend.BSG.Models.Profiles.Info;

namespace Fuyu.Backend.BSG.Models.Profiles;

[DataContract]
public class OtherProfileInfo
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
    public EMemberCategory SelectedMemberCategory { get; set; }


}