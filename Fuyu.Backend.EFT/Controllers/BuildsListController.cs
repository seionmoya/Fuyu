using System.Threading.Tasks;
using Fuyu.Common.Networking;
using Fuyu.Common.Serialization;
using Fuyu.Backend.BSG.DTO.Responses;
using Fuyu.Backend.EFT.DTO.Responses;

namespace Fuyu.Backend.EFT.Controllers
{
    public class BuildsListController : HttpController
    {
        public BuildsListController() : base("/client/builds/list")
        {
        }

        public override async Task RunAsync(HttpContext context)
        {
            await context.SendJsonAsync(Json.Stringify(new ResponseBody<BuildsListResponse>
            {
                data = new BuildsListResponse
                {
                    EquipmentBuild = [],
                    MagazineBuilds = [],
                    WeaponBuilds = []
                }
            }));
        }
    }
}