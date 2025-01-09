using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.Models.ItemEvents
{
    [DataContract]
    public class InsufficientNumberOfItemsData
    {
        [DataMember(Name = "itemId")]
        public string ItemId { get; set; }

        [DataMember(Name = "requestedCount")]
        public int RequestedCount { get; set; }

        [DataMember(Name = "actualCount")]
        public int ActualCount { get; set; }
    }
}