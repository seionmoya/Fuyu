using System.Threading.Tasks;
using Fuyu.Common.Networking;
using Fuyu.Common.Serialization;
using Fuyu.Backend.BSG.DTO.Responses;
using Fuyu.Backend.EFT.DTO.Requests;
using System.Linq;

namespace Fuyu.Backend.EFT.Controllers
{
    public class MatchLocalEndController : HttpController<MatchLocalEndRequest>
    {
        public MatchLocalEndController() : base("/client/match/local/end")
        {
        }

        public override async Task RunAsync(HttpContext context, MatchLocalEndRequest body)
        {
            var sessionId = context.GetSessionId();

            var profile = EftOrm.GetActiveProfile(sessionId);

            // NOTE: This data is not present in what the client sends as one of BSG's anticheat measures
            // which prevents your inraid inventory info from knowing what is in someone's stash
            // so I have to manually add the existing data that should be there which I think is ;ess effort
            // than manually taking the data that we want from the client's request
            // -- nexus4880, 2024-10-14
            body.results.profile.Info.LowerNickname = profile.Pmc.Info.LowerNickname;

            body.results.profile.Inventory.Stash = profile.Pmc.Inventory.Stash;
            body.results.profile.Inventory.QuestStashItems = profile.Pmc.Inventory.QuestStashItems;

            var stash = profile.Pmc.Inventory.Items.First(i => i.Id == profile.Pmc.Inventory.Stash);
            var questStashItems = profile.Pmc.Inventory.Items.First(i => i.Id == profile.Pmc.Inventory.QuestStashItems);

            body.results.profile.Inventory.Items = body.results.profile.Inventory.Items.Prepend(stash).Prepend(questStashItems).ToList();

            // save gear
            // TODO:
            // * scav / pmc state detection
            // -- seionmoya, 2024-08-28
            profile.Pmc = body.results.profile;
            EftOrm.SetOrAddProfile(profile);

            // send response
            var response = new ResponseBody<object>()
            {
                data = null
            };

            await context.SendJsonAsync(Json.Stringify(response));
        }
    }
}