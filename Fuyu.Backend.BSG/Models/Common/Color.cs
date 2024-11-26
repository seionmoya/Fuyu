using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.DTO.Common
{
	[DataContract]
	public struct Color
	{
		[DataMember(Name = "r")]
		public byte R { get; set; }

		[DataMember(Name = "g")]
		public byte G { get; set; }

		[DataMember(Name = "b")]
		public byte B { get; set; }

		[DataMember(Name = "a")]
		public byte A { get; set; }
	}
}