using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.Models.Locations
{
    [DataContract]
    public class MatchMakerWaitTime
    {
        [DataMember]
        public int time;

        [DataMember]
        public int minPlayers;
    }
}