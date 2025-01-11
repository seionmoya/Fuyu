using System.Threading.Tasks;
using Fuyu.Backend.BSG.Models.Requests;
using Fuyu.Backend.BSG.Models.Templates;
using Fuyu.Backend.BSG.Services;
using Fuyu.Backend.EFT.Networking;

namespace Fuyu.Backend.EFT.Controllers.Http;

public class ProfileWeaponBuildSaveController : AbstractEftHttpController<WeaponBuildSaveRequest>
{
    private readonly ResponseService _responseService;
    private readonly EftOrm _eftOrm;

    public ProfileWeaponBuildSaveController() : base("/client/builds/weapon/save")
    {
        _responseService = ResponseService.Instance;
        _eftOrm = EftOrm.Instance;
    }

    public override Task RunAsync(EftHttpContext context, WeaponBuildSaveRequest request)
    {
        var profile = _eftOrm.GetActiveProfile(context.SessionId);

        var weaponBuild = new WeaponBuild()
        {
            Id = request.Id,
            Name = request.Name,
            Root = request.Root,
            Items = request.Items
        };

        profile.Builds.WeaponBuilds.Add(weaponBuild);

        return context.SendJsonAsync(_responseService.EmptyJsonResponse, true, true);
    }
}