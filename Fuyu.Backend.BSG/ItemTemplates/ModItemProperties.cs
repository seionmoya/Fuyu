using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.ItemTemplates
{
    [DataContract]
    public class ModItemProperties : CompoundItemItemProperties
    {
        [DataMember(Name = "EffectiveDistance")]
        public int EffectiveDistance;

        [DataMember(Name = "Loudness")]
        public int Loudness;

        [DataMember(Name = "Accuracy")]
        public int Accuracy;

        [DataMember(Name = "DoubleActionAccuracyPenaltyMult")]
        public float DoubleActionAccuracyPenaltyMult;

        [DataMember(Name = "Recoil")]
        public float Recoil;

        [DataMember(Name = "Ergonomics")]
        public float Ergonomics;

        [DataMember(Name = "Velocity")]
        public float Velocity;

        [DataMember(Name = "RaidModdable")]
        public bool RaidModdable;

        [DataMember(Name = "ToolModdable")]
        public bool ToolModdable;

        [DataMember(Name = "BlocksFolding")]
        public bool BlocksFolding;

        [DataMember(Name = "BlocksCollapsible")]
        public bool BlocksCollapsible;

        [DataMember(Name = "IsAnimated")]
        public bool IsAnimated;

        [DataMember(Name = "SightingRange")]
        public float SightingRange;

        [DataMember(Name = "UniqueAnimationModID")]
        public int UniqueAnimationModID;

        [DataMember(Name = "HasShoulderContact")]
        public bool HasShoulderContact;
    }
}
