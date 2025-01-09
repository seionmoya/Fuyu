using System.Runtime.Serialization;
using Fuyu.Common.Hashing;

namespace Fuyu.Backend.BSG.ItemTemplates
{
    [DataContract]
    public class RepairKitsItemProperties : SpecItemItemProperties
    {
        [DataMember(Name = "MaxRepairResource")]
        public int MaxRepairResource;

        [DataMember(Name = "TargetItemFilter")]
        public MongoId[] TargetItemFilter;

        [DataMember(Name = "RepairQuality")]
        public float RepairQuality;

        [DataMember(Name = "RepairStrategyTypes")]
        public ERepairStrategyType[] RepairStrategyTypes;
    }

    public enum ERepairStrategyType
    {
        MeleeWeapon,
        Firearms,
        Armor
    }
}