using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.ItemTemplates
{
    [DataContract]
    public class StockItemProperties : GearModItemProperties
    {
        [DataMember(Name = "Foldable")]
        public bool Foldable;

        [DataMember(Name = "Retractable")]
        public bool Retractable;

        [DataMember(Name = "HeatFactor")]
        public float HeatFactor = 1f;

        [DataMember(Name = "CoolFactor")]
        public float CoolFactor = 1f;

        [DataMember(Name = "DurabilityBurnModificator")]
        public float DurabilityBurnModificator = 1f;

        [DataMember(Name = "SizeReduceRight")]
        public int SizeReduceRight;

        [DataMember(Name = "FoldedSlot", EmitDefaultValue = false)]
        public string FoldedSlot;
    }
}