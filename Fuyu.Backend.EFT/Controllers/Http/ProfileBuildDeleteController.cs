using System.Threading.Tasks;
using Fuyu.Backend.BSG.Models.Requests;
using Fuyu.Backend.BSG.Services;
using Fuyu.Backend.EFT.Networking;
using Fuyu.Common.IO;

namespace Fuyu.Backend.EFT.Controllers.Http;

public class ProfileBuildDeleteController : AbstractEftHttpController<BuildDeleteRequest>
{
    private readonly EftOrm _eftOrm;
    private readonly ResponseService _responseService;

    public ProfileBuildDeleteController() : base("/client/builds/delete")
    {
        _eftOrm = EftOrm.Instance;
        _responseService = ResponseService.Instance;
    }

    public override Task RunAsync(EftHttpContext context, BuildDeleteRequest request)
    {
        var profile = _eftOrm.GetActiveProfile(context.SessionId);

        var index = profile.Builds.EquipmentBuilds.RemoveAll(x => x.Id == request.Id);
        if (index < 1)
        {
            index = profile.Builds.WeaponBuilds.RemoveAll(x => x.Id == request.Id);
        }
        if (index < 1)
        {
            index = profile.Builds.MagazineBuilds.RemoveAll(x => x.Id == request.Id);
        }
        if (index < 1)
        {
            Terminal.WriteLine($"Could not find a build with the id {request.Id}");
        }


        return context.SendJsonAsync(_responseService.EmptyJsonResponse, true, true);
    }
}