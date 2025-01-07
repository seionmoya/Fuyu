using System.Threading.Tasks;
using System.Collections.Generic;
using Fuyu.Backend.BSG.Models.Customization;
using Fuyu.Backend.BSG.Models.Responses;
using Fuyu.Backend.EFT.Networking;
using Fuyu.Common.Serialization;

namespace Fuyu.Backend.EFT.Controllers.Http
{
    public class CustomizationController : EftHttpController
    {
        public CustomizationController() : base("/client/customization")
        {
        }

        public override Task RunAsync(EftHttpContext context)
        {
            var customizations = EftOrm.Instance.GetCustomizations();
            var response = new ResponseBody<Dictionary<string, CustomizationTemplate>>()
            {
                data = customizations
            };

            var text = Json.Stringify(response);
            return context.SendJsonAsync(text, true, true);
        }
    }
}