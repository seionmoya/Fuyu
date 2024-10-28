using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.DTO.Profiles
{
	[DataContract]
	public class Note
	{
		[DataMember(Name = "Time")]
		public float Time { get; set; }

		[DataMember(Name = "Text")]
		public string Text { get; set; }
	}
}