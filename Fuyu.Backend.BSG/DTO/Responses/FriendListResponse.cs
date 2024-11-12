using System.Runtime.Serialization;
using Fuyu.Backend.BSG.DTO.Friends;
using Fuyu.Common.Hashing;

namespace Fuyu.Backend.EFT.DTO.Responses
{
    [DataContract]
    public class FriendListResponse
    {
        [DataMember]
        public ChatRoomMember[] Friends;

        [DataMember]
        public MongoId[] Ignore;

        [DataMember]
        public MongoId[] InIgnoreList;
    }
}