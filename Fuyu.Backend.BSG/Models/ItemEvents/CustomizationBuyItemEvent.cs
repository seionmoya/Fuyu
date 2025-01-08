using Fuyu.Backend.BSG.Models.Trading;
using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.Models.ItemEvents
{
    [DataContract]
    public class CustomizationBuyItemEvent : BaseItemEvent
    {
        [DataMember(Name = "offer")]
        public string Offer { get; set; }
        [DataMember(Name = "items")]
        public CustomizationOfferInfo[] Items { get; set; }
    }
}
