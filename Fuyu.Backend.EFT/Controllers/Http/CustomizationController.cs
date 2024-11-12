using System.Threading.Tasks;
using System.Collections.Generic;
using Fuyu.Common.Networking;
using Fuyu.Common.Serialization;
using Fuyu.Backend.BSG.Models.Customization;
using Fuyu.Backend.BSG.Models.Responses;

namespace Fuyu.Backend.EFT.Controllers.Http
{
    public class CustomizationController : HttpController
    {
        public CustomizationController() : base("/client/customization")
        {
        }

        public override async Task RunAsync(HttpContext context)
        {
            var customizations = EftOrm.GetCustomizations();
            var response = new ResponseBody<Dictionary<string, CustomizationTemplate>>()
            {
                data = customizations
            };

            await context.SendJsonAsync(Json.Stringify(response));
        }
    }
}