using System.Runtime.Serialization;
using Fuyu.Common.Hashing;

namespace Fuyu.Backend.BSG.Models.ItemEvents
{
    [DataContract]
    public class RepairItemEvent : BaseItemEvent
    {
        [DataMember(Name = "repairKitsInfo")]
        public RepairItem[] RepairKitsInfo { get; set; }

        [DataMember(Name = "target")]
        public MongoId TargetItemId { get; set; }
    }

    [DataContract]
    public class RepairItem
    {
        [DataMember(Name = "_id")]
        public MongoId Id { get; set; }

        [DataMember(Name = "count")]
        public float Count { get; set; }
    }
}