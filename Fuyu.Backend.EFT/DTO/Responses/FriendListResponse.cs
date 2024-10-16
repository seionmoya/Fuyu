using System.Runtime.Serialization;
using Fuyu.Backend.BSG.DTO.Friends;
using Fuyu.Common.Hashing;

namespace Fuyu.Backend.EFT.DTO.Responses
{
    [DataContract]
    public class FriendListResponse
    {
        // TODO: proper type
        [DataMember]
        public ChatRoomMember[] Friends;

        // TODO: proper type
        [DataMember]
        public MongoId[] Ignore;

        // TODO: proper type
        [DataMember]
        public MongoId[] InIgnoreList;
    }
}