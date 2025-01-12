using System.Threading.Tasks;
using Fuyu.Backend.EFTMain.Networking;

namespace Fuyu.Backend.EFTMain.Controllers.Http;

public class HideoutQteListController : AbstractEftHttpController
{
    private readonly EftOrm _eftOrm;

    public HideoutQteListController() : base("/client/hideout/qte/list")
    {
        _eftOrm = EftOrm.Instance;
    }

    public override Task RunAsync(EftHttpContext context)
    {
        // TODO: generate this
        // --seionmoya, 2024-11-18
        var response = _eftOrm.GetHideoutQtes();
        var text = response.ToString();
        return context.SendJsonAsync(text, true, true);
    }
}