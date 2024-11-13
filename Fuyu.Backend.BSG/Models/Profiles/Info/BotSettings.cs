using System.Runtime.Serialization;
using Newtonsoft.Json.Converters;
using Fuyu.Backend.BSG.Models.Bots;

namespace Fuyu.Backend.BSG.Models.Profiles.Info
{
    [DataContract]
    public class BotSettings
    {
        [DataMember]
        [Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
        public EBotRole Role;

        [DataMember]
        [Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
        public EBotDifficulty BotDifficulty;

        [DataMember]
        public int Experience;

        [DataMember]
        public float StandingForKill;

        [DataMember]
        public float AggressorBonus;
    }
}