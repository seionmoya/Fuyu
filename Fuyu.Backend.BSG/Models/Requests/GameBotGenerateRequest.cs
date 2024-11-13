using System.Runtime.Serialization;
using Fuyu.Backend.BSG.Models.Bots;

namespace Fuyu.Backend.BSG.Models.Requests
{
    [DataContract]
    public class GameBotGenerateRequest
    {
        [DataMember]
        public BotCondition[] conditions;
    }
}