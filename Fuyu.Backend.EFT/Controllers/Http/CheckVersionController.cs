using System.Threading.Tasks;
using Fuyu.Backend.BSG.Models.Responses;
using Fuyu.Common.Networking;
using Fuyu.Common.Serialization;

namespace Fuyu.Backend.EFT.Controllers.Http
{
    public class CheckVersionController : HttpController
    {
        public CheckVersionController() : base("/client/checkVersion")
        {
        }

        public override Task RunAsync(HttpContext context)
        {
            var response = new ResponseBody<CheckVersionResponse>()
            {
                data = new CheckVersionResponse()
                {
                    isvalid = true,
                    latestVersion = "0.15.0.2.32197"
                }
            };

            return context.SendJsonAsync(Json.Stringify(response));
        }
    }
}