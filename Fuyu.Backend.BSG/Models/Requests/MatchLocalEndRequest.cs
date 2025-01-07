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
        public string serverId { get; set; }

        [DataMember]
        public MatchLocalEndResult results { get; set; }

        [DataMember]
        public ItemInstance[] lostInsuredItems { get; set; }

        [DataMember]
        public Dictionary<string, ItemInstance[]> transferItems { get; set; }

        // TODO: proper type
        [DataMember]
        public object locationTransit { get; set; }
    }
}