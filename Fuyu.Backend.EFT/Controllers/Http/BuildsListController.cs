using System.Threading.Tasks;
using Fuyu.Backend.BSG.Models.Responses;
using Fuyu.Backend.EFT.Networking;
using Fuyu.Common.IO;
using Fuyu.Common.Serialization;

namespace Fuyu.Backend.EFT.Controllers.Http
{
    public class BuildsListController : EftHttpController
    {
        private readonly ResponseBody<BuildsListResponse> _response;

        public BuildsListController() : base("/client/builds/list")
        {
            var json = Resx.GetText("eft", "database.client.builds.list.json");
            _response = Json.Parse<ResponseBody<BuildsListResponse>>(json);
        }

        public override Task RunAsync(EftHttpContext context)
        {
            // TODO: generate this
            // --seionmoya, 2024-11-18
            var text = Json.Stringify(_response);
            return context.SendJsonAsync(text, true, true);
        }
    }
}