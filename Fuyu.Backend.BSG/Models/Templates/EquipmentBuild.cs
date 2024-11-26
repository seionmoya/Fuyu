using System.Runtime.Serialization;
using Fuyu.Backend.BSG.Models.Items;
using Fuyu.Common.Hashing;

namespace Fuyu.Backend.BSG.Models.Templates
{
    [DataContract]
    public class EquipmentBuild
    {
        [DataMember(Name = "Id")]
        public MongoId Id { get; set; }

        [DataMember(Name = "Name")]
        public string Name { get; set; }

		[DataMember(Name = "Root")]
		public MongoId Root { get; set; }

		[DataMember(Name = "Items")]
        public ItemInstance[] Items { get; set; }

		[DataMember(Name = "BuildType")]
        public EEquipmentBuildType BuildType { get; set; }
	}
}