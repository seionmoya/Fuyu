using System.Runtime.Serialization;
using Fuyu.Backend.BSG.Models.Responses;
using Fuyu.Backend.BSG.Models.Templates;
using Fuyu.Common.Hashing;

namespace Fuyu.Backend.BSG.Models.Items;

[DataContract]
public class MagazineBuild
{
    [DataMember(Name = "Id")]
    public MongoId Id { get; set; }

    [DataMember(Name = "Name")]
    public string Name { get; set; }

    [DataMember(Name = "Caliber")]
    public string Caliber { get; set; }

    [DataMember(Name = "BuildType")]
    public EEquipmentBuildType BuildType { get; set; }

    [DataMember(Name = "Items")]
    public MagazineItem[] Items { get; set; }
}