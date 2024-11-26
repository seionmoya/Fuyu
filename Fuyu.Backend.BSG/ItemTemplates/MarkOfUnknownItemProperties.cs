using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.ItemTemplates
{
    [DataContract]
    public class MarkOfUnknownItemProperties : SpecItemItemProperties
    {
        [DataMember(Name = "TradersDiscount")]
        public float TradersDiscount { get; set; }

        [DataMember(Name = "ScavKillExpPenalty")]
        public float ScavKillExpPenalty { get; set; }

        [DataMember(Name = "ScavKillStandingPenalty")]
        public float ScavKillStandingPenalty { get; set; }
    }
}
