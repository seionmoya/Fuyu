using System.Threading.Tasks;
using Fuyu.Common.Networking;
using Fuyu.Common.Serialization;
using Fuyu.Backend.BSG.DTO.Responses;
using Fuyu.Backend.EFT.DTO.Responses;
using Fuyu.Backend.BSG.DTO.Friends;
using Fuyu.Common.Hashing;
using Fuyu.Backend.BSG.DTO.Profiles.Info;
using System;

namespace Fuyu.Backend.EFT.Controllers
{
    public class FriendListController : HttpController
    {
        public FriendListController() : base("/client/friend/list")
        {
        }

        public override async Task RunAsync(HttpContext context)
        {
            var response = new ResponseBody<FriendListResponse>()
            {
                data = new FriendListResponse()
                {
                    Friends = [
                        new ChatRoomMember
                        {
                            Id = new MongoId(true),
                            AccountId = int.MaxValue,
                            Info = new ChatRoomMemberInfo
                            {
                                MemberCategory = EMemberCategory.Default,
                                SelectedMemberCategory = EMemberCategory.Default,
                                Banned = false,
                                Ignored = false,
                                Level = 31,
                                Nickname = "b1gsteppa",
                                Side = EChatMemberSide.Usec,
                                GlobalMuteTime = DateTime.UtcNow.AddSeconds(10d)
                            }
                        }
                        ],
                    Ignore = [],
                    InIgnoreList = []
                }
            };

            await context.SendJsonAsync(Json.Stringify(response));
        }
    }
}