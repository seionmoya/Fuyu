using System.Threading.Tasks;
using Fuyu.Backend.BSG.Models.Customization;
using Fuyu.Backend.BSG.Models.Responses;
using Fuyu.Backend.EFT.Networking;
using Fuyu.Common.Serialization;

namespace Fuyu.Backend.EFT.Controllers.Http
{
    public class CustomizationStorageController : AbstractEftHttpController
    {
        public CustomizationStorageController() : base("/client/customization/storage")
        {
        }

        public override Task RunAsync(EftHttpContext context)
        {
            var response = new ResponseBody<CustomizationStorageEntry[]>()
            {
                data = EftOrm.Instance.GetCustomizationStorage().ToArray()
            };

            var text = Json.Stringify(response);
            return context.SendJsonAsync(text, true, true);
        }
    }
}