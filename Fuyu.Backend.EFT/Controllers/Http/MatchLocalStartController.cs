using System.Threading.Tasks;
using Fuyu.Backend.BSG.Models.Requests;
using Fuyu.Backend.EFT.Networking;
using Fuyu.Backend.EFT.Services;

namespace Fuyu.Backend.EFT.Controllers.Http
{
    public class MatchLocalStartController : EftHttpController<MatchLocalStartRequest>
    {
        public MatchLocalStartController() : base("/client/match/local/start")
        {
        }

        public override Task RunAsync(EftHttpContext context, MatchLocalStartRequest request)
        {
            var location = request.location;
            var text = LocationService.Instance.GetLoot(location);
            return context.SendJsonAsync(text, true, true);
        }
    }
}