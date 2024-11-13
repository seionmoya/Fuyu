using System.Runtime.Serialization;
using Fuyu.Backend.BSG.Models.Friends;
using Fuyu.Common.Hashing;

namespace Fuyu.Backend.BSG.Models.Responses
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