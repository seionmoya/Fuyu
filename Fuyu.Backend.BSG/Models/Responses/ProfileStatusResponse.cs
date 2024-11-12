using System.Runtime.Serialization;
using Fuyu.Backend.BSG.Models.Multiplayer;

namespace Fuyu.Backend.BSG.Models.Responses
{
    [DataContract]
    public class ProfileStatusResponse
    {
        [DataMember]
        public bool maxPveCountExceeded;

        [DataMember]
        public ProfileStatusInfo[] profiles;
    }
}