using System.Runtime.Serialization;
using Fuyu.Backend.EFT.DTO.Templates;
using Fuyu.Common.Hashing;

namespace Fuyu.Backend.EFT.DTO.Responses
{
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
    public class WeaponBuild
    {
		[DataMember(Name = "Id")]
		public MongoId Id { get; set; }

		[DataMember(Name = "ItemIconName")]
		public string ItemIconName { get; set; }

		[DataMember(Name = "HandbookName")]
		public string HandbookName { get; set; }

		[DataMember(Name = "FromPreset")]
		public bool FromPreset { get; set; }
	}

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

	[DataContract]
	public class MagazineItem
	{
		[DataMember(Name = "TemplateId")]
		public MongoId TemplateId { get; set; }

		[DataMember(Name = "Count")]
		public ushort Count { get; set; }
	}
}