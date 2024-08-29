using Fuyu.Platform.Common.Http;
using Fuyu.Platform.Common.Models.EFT.Profiles;
using Fuyu.Platform.Common.Models.EFT.Responses;
using Fuyu.Platform.Common.Serialization;
using Fuyu.Platform.Server.Databases;

namespace Fuyu.Platform.Server.Behaviours.EFT
{
    public class GameProfileList : FuyuBehaviour
    {
        public override void Run(FuyuContext context)
        {
            var sessionId = context.GetSessionId();
            var account = FuyuDatabase.Accounts.GetAccount(sessionId);

            // TODO: PVP-PVE STATE DETECTION
            var pve = account.EftSave.PvE;
            Profile[] profiles = pve.ShouldWipe
                ? []
                : [pve.Pmc, pve.Savage];

            var response = new ResponseBody<Profile[]>()
            {
                data = profiles
            };

            SendJson(context, Json.Stringify(response));
        }
    }
}