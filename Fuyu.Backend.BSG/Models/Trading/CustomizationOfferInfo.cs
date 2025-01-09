using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.Models.Trading
{
    [DataContract]
    public class CustomizationOfferInfo
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }
        [DataMember(Name = "count")]
        public int Count { get; set; }
    }
}