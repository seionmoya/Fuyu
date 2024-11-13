using System.Runtime.Serialization;
using Fuyu.Backend.BSG.Models.Raid;

namespace Fuyu.Backend.BSG.Models.Responses
{
    [DataContract]
    public class MatchGroupCurrentResponse
    {
        [DataMember]
        public SquadMember[] squad;

        // TODO: proper type
        [DataMember]
        public object raidSettings;
    }
}