using System.Threading.Tasks;
using Fuyu.Backend.BSG.Models.Responses;
using Fuyu.Common.IO;
using Fuyu.Common.Networking;
using Fuyu.Common.Serialization;

namespace Fuyu.Backend.EFT.Controllers.Http
{
    public class BuildsListController : HttpController
    {
        private readonly ResponseBody<BuildsListResponse> _response;

        public BuildsListController() : base("/client/builds/list")
        {
            var json = Resx.GetText("eft", "database.client.builds.list.json");
            _response = Json.Parse<ResponseBody<BuildsListResponse>>(json);
        }

        public override Task RunAsync(HttpContext context)
        {
            return context.SendJsonAsync(Json.Stringify(_response));
        }
    }
}