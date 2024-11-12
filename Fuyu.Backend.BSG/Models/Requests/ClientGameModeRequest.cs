using System.Runtime.Serialization;
using Fuyu.Backend.EFT.DTO.Accounts;

namespace Fuyu.Backend.BSG.Models.Requests
{
	[DataContract]
	public class ClientGameModeRequest
	{
		[DataMember(Name = "sessionMode")]
		public ESessionMode? SessionMode { get; set; }
	}
}
