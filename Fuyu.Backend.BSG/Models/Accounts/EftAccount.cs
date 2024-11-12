using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.Models.Accounts
{
    [DataContract]
    public class EftAccount
    {
        [DataMember]
        public int Id { get; set; }

		[DataMember]
        public string Edition { get; set; }

		[DataMember]
        public string Username { get; set; }

		[DataMember]
        public string PvpId { get; set; }

		[DataMember]
        public string PveId { get; set; }

		[DataMember]
        public ESessionMode? CurrentSession { get; set; }
    }
}