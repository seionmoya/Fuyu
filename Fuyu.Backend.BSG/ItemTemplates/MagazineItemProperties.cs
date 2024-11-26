using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.ItemTemplates
{
    [DataContract]
    public class MagazineItemProperties : GearModItemProperties
    {
        [DataMember(Name = "magAnimationIndex")]
        public int magAnimationIndex;

        [DataMember(Name = "Cartridges")]
        public object[] Cartridges;

        [DataMember(Name = "LoadUnloadModifier")]
        public float LoadUnloadModifier;

        [DataMember(Name = "CheckTimeModifier")]
        public float CheckTimeModifier;

        [DataMember(Name = "CheckOverride")]
        public int CheckOverride;

        [DataMember(Name = "ReloadMagType")]
        public EReloadMode ReloadMagType;

        [DataMember(Name = "VisibleAmmoRangesString")]
        public string VisibleAmmoRangesString;

        [DataMember(Name = "MalfunctionChance")]
        public float MalfunctionChance;

        [DataMember(Name = "MagazineWithBelt")]
        public bool MagazineWithBelt;

        [DataMember(Name = "BeltMagazineRefreshCount")]
        public int BeltMagazineRefreshCount;

        [DataMember(Name = "IsMagazineForStationaryWeapon")]
        public bool IsMagazineForStationaryWeapon;
    }

    public enum EReloadMode
    {
        ExternalMagazine,
        InternalMagazine,
        OnlyBarrel,
        ExternalMagazineWithInternalReloadSupport
    }
}
