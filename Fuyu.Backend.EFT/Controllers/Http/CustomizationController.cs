using System.Threading.Tasks;
using System.Collections.Generic;
using Fuyu.Backend.BSG.Models.Customization;
using Fuyu.Backend.BSG.Models.Responses;
using Fuyu.Common.Networking;
using Fuyu.Common.Serialization;

namespace Fuyu.Backend.EFT.Controllers.Http
{
    public class CustomizationController : HttpController
    {
        public CustomizationController() : base("/client/customization")
        {
        }

        public override Task RunAsync(HttpContext context)
        {
            var customizations = EftOrm.GetCustomizations();
            var response = new ResponseBody<Dictionary<string, CustomizationTemplate>>()
            {
                data = customizations
            };

            return context.SendJsonAsync(Json.Stringify(response));
        }
    }
}