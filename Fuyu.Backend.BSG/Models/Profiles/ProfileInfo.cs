using System.Runtime.Serialization;
using Fuyu.Backend.BSG.Models.Profiles.Info;
using Newtonsoft.Json.Converters;

namespace Fuyu.Backend.BSG.Models.Profiles
{
    [DataContract]
    public class ProfileInfo
    {
        [DataMember]
        public string Nickname;

        // NOTE: only available when player is scav
        // -- seionmoya, 2024-10-07
        [DataMember(EmitDefaultValue = false)]
        public string MainProfileNickname { get; set; }

        [DataMember]
        public string LowerNickname { get; set; }

        [DataMember]
        [Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
        public EPlayerSide Side { get; set; }

        [DataMember]
        public string Voice { get; set; }

        [DataMember]
        public int Level { get; set; }

        [DataMember]
        public long Experience { get; set; }

        [DataMember]
        public long RegistrationDate { get; set; }

        [DataMember]
        public string GameVersion { get; set; }

        // SKIPPED: AccountType
        // Reason: only used on BSG's internal server

        [DataMember]
        public EMemberCategory MemberCategory { get; set; }

        [DataMember]
        public EMemberCategory SelectedMemberCategory { get; set; }

        // SKIPPED: LockedMoveCommands
        // Reason: only used on BSG's internal server

        [DataMember]
        public long SavageLockTime { get; set; }

        // NOTE: used in /client/match/local/end, not sure when emitted
        // -- seionmoya, 2024-10-07
        [DataMember(EmitDefaultValue = false)]
        public string GroupId { get; set; }

        // NOTE: used in /client/match/local/end, not sure when emitted
        // -- seionmoya, 2024-10-07
        [DataMember(EmitDefaultValue = false)]
        public string TeamId { get; set; }

        [DataMember]
        public long LastTimePlayedAsSavage { get; set; }

        [DataMember]
        public BotSettings Settings { get; set; }

        [DataMember]
        public long NicknameChangeDate { get; set; }

        // SKIPPED: NeedWipeOptions
        // Reason: only used on BSG's internal server

        // SKIPPED: LastCompletedWipe
        // Reason: only used on BSG's internal server

        // SKIPPED: LastCompletedEvent
        // Reason: only used on BSG's internal server

        [DataMember]
        public bool BannedState { get; set; }

        [DataMember]
        public long BannedUntil { get; set; }

        [DataMember]
        public bool IsStreamerModeAvailable { get; set; }

        [DataMember]
        public bool SquadInviteRestriction { get; set; }

        [DataMember]
        public bool HasCoopExtension { get; set; }

        [DataMember]
        public bool isMigratedSkills { get; set; }

        [DataMember]
        public bool HasPveGame { get; set; }

        [DataMember]
        public int PrestigeLevel { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public Ban[] Bans { get; set; }
    }
}