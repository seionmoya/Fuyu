using System.Threading.Tasks;
using Fuyu.Backend.BSG.Models.Requests;
using Fuyu.Backend.BSG.Models.Templates;
using Fuyu.Backend.BSG.Services;
using Fuyu.Backend.EFT.Networking;

namespace Fuyu.Backend.EFT.Controllers.Http;

public class ProfileEquipmentBuildSaveController : AbstractEftHttpController<EquipmentBuildSaveRequest>
{
    private readonly ResponseService _responseService;
    private readonly EftOrm _eftOrm;

    public ProfileEquipmentBuildSaveController() : base("/client/builds/equipment/save")
    {
        _responseService = ResponseService.Instance;
        _eftOrm = EftOrm.Instance;
    }

    public override Task RunAsync(EftHttpContext context, EquipmentBuildSaveRequest request)
    {
        var equipmentBuild = new EquipmentBuild()
        {
            Id = request.Id,
            Name = request.Name,
            Root = request.Root,
            Items = request.Items,
            BuildType = EEquipmentBuildType.Custom
        };

        var profile = _eftOrm.GetActiveProfile(context.SessionId);
        profile.Builds.EquipmentBuilds.Add(equipmentBuild);

        return context.SendJsonAsync(_responseService.EmptyJsonResponse, true, true);
    }
}