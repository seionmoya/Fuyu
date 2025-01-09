using System.Threading.Tasks;
using Fuyu.Backend.EFT.Networking;

namespace Fuyu.Backend.EFT.Controllers.Http;

public class SettingsController : AbstractEftHttpController
{
    private readonly EftOrm _eftOrm;

    public SettingsController() : base("/client/settings")
    {
        _eftOrm = EftOrm.Instance;
    }

    public override Task RunAsync(EftHttpContext context)
    {
        // TODO: generate this
        // --seionmoya, 2024-11-18
        var response = _eftOrm.GetSettings();
        var text = response.ToString();
        return context.SendJsonAsync(text, true, true);
    }
}