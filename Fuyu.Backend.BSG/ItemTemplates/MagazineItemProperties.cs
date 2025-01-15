using System.Collections.Generic;
using System.Runtime.Serialization;
using Fuyu.Common.Hashing;

namespace Fuyu.Backend.BSG.ItemTemplates;

[DataContract]
public class MagazineItemProperties : GearModItemProperties
{
    [DataMember(Name = "magAnimationIndex")]
    public int MagAnimationIndex { get; set; }

    [DataMember(Name = "Cartridges")]
    public List<MagazineCartridge> Cartridges { get; set; }

    [DataMember(Name = "LoadUnloadModifier")]
    public float LoadUnloadModifier { get; set; }

    [DataMember(Name = "CheckTimeModifier")]
    public float CheckTimeModifier { get; set; }

    [DataMember(Name = "CheckOverride")]
    public int CheckOverride { get; set; }

    [DataMember(Name = "ReloadMagType")]
    public EReloadMode ReloadMagType { get; set; }

    [DataMember(Name = "VisibleAmmoRangesString")]
    public string VisibleAmmoRangesString { get; set; }

    [DataMember(Name = "MalfunctionChance")]
    public float MalfunctionChance { get; set; }

    [DataMember(Name = "MagazineWithBelt")]
    public bool MagazineWithBelt { get; set; }

    [DataMember(Name = "BeltMagazineRefreshCount")]
    public int BeltMagazineRefreshCount { get; set; }

    [DataMember(Name = "IsMagazineForStationaryWeapon")]
    public bool IsMagazineForStationaryWeapon { get; set; }
}

public enum EReloadMode
{
    ExternalMagazine,
    InternalMagazine,
    OnlyBarrel,
    ExternalMagazineWithInternalReloadSupport
}

[DataContract]
public class MagazineCartridge
{
    [DataMember(Name = "_name")]
    public string Name { get; set; }

    [DataMember(Name = "_id")]
    public MongoId Id { get; set; }

    [DataMember(Name = "_parent")]
    public MongoId Parent { get; set; }

    [DataMember(Name = "max_count")]
    public int MaxCount { get; set; }

    [DataMember(Name = "_props")]
    public SlotProperties Properties { get; set; }
}