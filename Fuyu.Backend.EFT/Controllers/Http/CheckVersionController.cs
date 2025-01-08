using System.Threading.Tasks;
using Fuyu.Backend.BSG.Models.Responses;
using Fuyu.Backend.EFT.Networking;
using Fuyu.Common.Serialization;

namespace Fuyu.Backend.EFT.Controllers.Http
{
    public class CheckVersionController : AbstractEftHttpController
    {
        public CheckVersionController() : base("/client/checkVersion")
        {
        }

        public override Task RunAsync(EftHttpContext context)
        {
            // TODO: handle this
            // --seionmoya, 2024-11-18
            var response = new ResponseBody<CheckVersionResponse>()
            {
                data = new CheckVersionResponse()
                {
                    isvalid = true,
                    latestVersion = "0.16.0.2.34510"
                }
            };

            var text = Json.Stringify(response);
            return context.SendJsonAsync(text, true, true);
        }
    }
}