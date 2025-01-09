using System.Collections.Generic;
using System.Threading.Tasks;
using Fuyu.Backend.BSG.Models.Customization;
using Fuyu.Backend.BSG.Models.Responses;
using Fuyu.Backend.EFT.Networking;
using Fuyu.Common.Serialization;

namespace Fuyu.Backend.EFT.Controllers.Http;

public class CustomizationController : AbstractEftHttpController
{
    private readonly EftOrm _eftOrm;

    public CustomizationController() : base("/client/customization")
    {
        _eftOrm = EftOrm.Instance;
    }

    public override Task RunAsync(EftHttpContext context)
    {
        var customizations = _eftOrm.GetCustomizations();
        var response = new ResponseBody<Dictionary<string, CustomizationTemplate>>()
        {
            data = customizations
        };

        var text = Json.Stringify(response);
        return context.SendJsonAsync(text, true, true);
    }
}