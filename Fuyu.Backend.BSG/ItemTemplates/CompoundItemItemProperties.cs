using System.Runtime.Serialization;
using System.Runtime.Serialization.DataContracts;
using Fuyu.Common.Hashing;

namespace Fuyu.Backend.BSG.ItemTemplates;

[DataContract]
public class CompoundItemItemProperties : ItemProperties
{
    [DataMember(Name = "Grids")]
    public Grid[] Grids = [];

    [DataMember(Name = "Slots")]
    public Slot[] Slots = [];

    [DataMember(Name = "CanPutIntoDuringTheRaid")]
    public bool CanPutIntoDuringTheRaid;

    [DataMember(Name = "CantRemoveFromSlotsDuringRaid")]
    public EEquipmentSlot[] CantRemoveFromSlotsDuringRaid = [];

    [DataMember(Name = "ForbidMissingVitalParts")]
    public bool ForbidMissingVitalParts;

    [DataMember(Name = "ForbidNonEmptyContainers")]
    public bool ForbidNonEmptyContainers;
}

public enum EEquipmentSlot
{
    FirstPrimaryWeapon,
    SecondPrimaryWeapon,
    Holster,
    Scabbard,
    Backpack,
    SecuredContainer,
    TacticalVest,
    ArmorVest,
    Pockets,
    Eyewear,
    FaceCover,
    Headwear,
    Earpiece,
    Dogtag,
    ArmBand
}

[DataContract]
public class Slot
{
    [DataMember(Name = "_name")]
    public string Name { get; set; }

    [DataMember(Name = "_id")]
    public MongoId Id { get; set; }

    [DataMember(Name = "_parent")]
    public MongoId Parent { get; set; }

    [DataMember(Name = "_props")]
    public SlotProperties Properties { get; set; }

    [DataMember(Name = "_required")]
    public bool Required { get; set; }

    [DataMember(Name = "_mergeSlotWithChildren")]
    public bool MergeSlotWithChildren { get; set; }

    [DataMember(Name = "_proto")]
    // I am hesitant to mark this as MongoId even though it seemingly is
    // because I am not nor have I ever been sure of what proto even is
    // -- nexus4880, 2024-10-18
    public string Proto { get; set; }
}

[DataContract]
public class SlotProperties
{
    [DataMember(Name = "filters")]
    public SlotPropertiesFilter[] Filters { get; set; }
}

[DataContract]
public class SlotPropertiesFilter
{
    [DataMember(Name = "Shift")]
    public int Shift { get; set; }

    [DataMember(Name = "Filter")]
    public MongoId[] Filter { get; set; }
}

[DataContract]
public class Grid
{
    [DataMember(Name = "_name")]
    public string Name { get; set; }

    [DataMember(Name = "_id")]
    public MongoId Id { get; set; }

    [DataMember(Name = "_parent")]
    public MongoId Parent { get; set; }

    [DataMember(Name = "_props")]
    public GridProperties Properties { get; set; }

    [DataMember(Name = "_proto")]
    public string Proto { get; set; }
}

[DataContract]
public class GridProperties
{
    [DataMember(Name = "filters")]
    public GridPropertiesFilter[] Filters { get; set; }

    [DataMember(Name = "cellsH")]
    public int CellsHorizontal { get; set; }

    [DataMember(Name = "cellsV")]
    public int CellsVertical { get; set; }

    [DataMember(Name = "minCount")]
    public int MinCount { get; set; }

    [DataMember(Name = "maxCount")]
    public int MaxCount { get; set; }

    [DataMember(Name = "maxWeight")]
    public int MaxWeight { get; set; }

    [DataMember(Name = "isSortingTable")]
    public bool IsSortingTable { get; set; }
}

[DataContract]
public class GridPropertiesFilter
{
    [DataMember(Name = "Filter")]
    public MongoId[] Filter { get; set; }

    [DataMember(Name = "ExcludedFilter")]
    public MongoId[] ExcludedFilter { get; set; }
}