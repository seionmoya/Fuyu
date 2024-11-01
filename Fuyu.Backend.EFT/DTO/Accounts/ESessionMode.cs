using System.Runtime.Serialization;

namespace Fuyu.Backend.EFT.DTO.Accounts
{
	[DataContract]
	public enum ESessionMode
	{
		[EnumMember(Value = "regular")]
		Regular,
		[EnumMember(Value = "pve")]
		Pve
	}
}
