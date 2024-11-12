using System.Collections.Generic;
using System.Runtime.Serialization;
using Fuyu.Backend.BSG.Models.Items;
using Fuyu.Backend.BSG.Models.Raid;

namespace Fuyu.Backend.BSG.Models.Requests
{
    [DataContract]
    public class MatchLocalEndRequest
    {
        [DataMember]
        public string serverId;

        [DataMember]
        public MatchLocalEndResult results;

        [DataMember]
        public ItemInstance[] lostInsuredItems;

        [DataMember]
        public Dictionary<string, ItemInstance[]> transferItems;

        // TODO: proper type
        [DataMember]
        public object locationTransit;
    }
}