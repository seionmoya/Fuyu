using System.Runtime.Serialization;
using Fuyu.Common.Hashing;

namespace Fuyu.Backend.BSG.DTO.Friends
{
    [DataContract]
    public class ChatRoomMember
    {
        [DataMember(Name = "_id")]
        public MongoId Id { get; set; }

        [DataMember(Name = "Info")]
        public ChatRoomMemberInfo Info { get; set; }

        // NOTE: This is a string on the client but I don't
        // want to have to do .ToString() on the server
        // -- nexus4880, 2024-10-16
        [DataMember(Name = "aid")]
        public int? AccountId { get; set; }
    }
}
