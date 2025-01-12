using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.Models.Profiles;

public enum ENicknameChangeResult
{
    [DataMember(Name = "wrongSymbol")]
    WrongSymbol,
    [DataMember(Name = "tooShort")]
    TooShort,
    [DataMember(Name = "characterLimit")]
    CharacterLimit,
    [DataMember(Name = "invalidNickname")]
    InvalidNickname,
    [DataMember(Name = "nicknameTaken")]
    NicknameTaken,
    [DataMember(Name = "nicknameChangeTimeout")]
    NicknameChangeTimeout,
    [DataMember(Name = "digitsLimit")]
    DigitsLimit,
    [DataMember(Name = "ok")]
    Ok
}