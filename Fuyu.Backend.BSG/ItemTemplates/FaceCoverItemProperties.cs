using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.ItemTemplates
{
	[DataContract]
	public class FaceCoverItemProperties : ArmoredEquipmentItemProperties
	{
		[DataMember(Name = "IsHalfMask")]
		public bool IsHalfMask { get; set; }
	}
}
