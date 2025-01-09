using System.Runtime.Serialization;
using Fuyu.Backend.BSG.Models.Items;
using Fuyu.Backend.BSG.Models.Templates;
using Fuyu.Common.Hashing;

namespace Fuyu.Backend.BSG.Models.Responses;

[DataContract]
public class BuildsListResponse
{
    [DataMember(Name = "equipmentBuilds")]
    public EquipmentBuild[] EquipmentBuild { get; set; }

    [DataMember(Name = "weaponBuilds")]
    public WeaponBuild[] WeaponBuilds { get; set; }

    [DataMember(Name = "magazineBuilds")]
    public MagazineBuild[] MagazineBuilds { get; set; }
}

[DataContract]
public class MagazineItem
{
    [DataMember(Name = "TemplateId")]
    public MongoId TemplateId { get; set; }

    [DataMember(Name = "Count")]
    public ushort Count { get; set; }
}