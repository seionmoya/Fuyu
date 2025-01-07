using System.Collections.Generic;
using System.Runtime.Serialization;
using Fuyu.Backend.BSG.Models.Profiles.Quests;
using Fuyu.Common.Hashing;

namespace Fuyu.Backend.BSG.Models.Profiles
{
    [DataContract]
    public class QuestInfo
    {
        [DataMember]
        public MongoId qid { get; set; }

        [DataMember]
        public long startTime { get; set; }

        [DataMember]
        public EQuestStatus status { get; set; }

        [DataMember]
        public Dictionary<string, long> statusTimers { get; set; }
    }
}