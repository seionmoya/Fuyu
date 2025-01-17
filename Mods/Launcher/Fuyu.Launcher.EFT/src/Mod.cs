using System.IO;
using System.Threading.Tasks;
using Fuyu.Common.IO;
using Fuyu.Common.Networking;
using Fuyu.Common.Services;
using Fuyu.DependencyInjection;
using Fuyu.Common.Launcher.Services;
using Fuyu.Launcher.EFT.Pages;
using Fuyu.Modding;

namespace Fuyu.Launcher.EFT;

public class Mod : AbstractMod
{
    public override string Id { get; } = "Fuyu.Launcher.EFT";
    public override string Name { get; } = "Fuyu.Launcher.EFT";
    public override string[] Dependencies { get; } = [
        "Fuyu.Launcher.Core"
    ];

    private ContentService _contentService;
    private RequestService _requestService;

    public override Task OnLoad(DependencyContainer container)
    {
        // resolve dependencies
        _contentService = ContentService.Instance;
        _requestService = RequestService.Instance;

        Resx.SetSource(Id, this.GetType().Assembly);

        var eftHttpClient = new HttpClient("http://localhost:8010");
        _requestService.AddOrSetClient("eft", eftHttpClient);

        InitializePages();
        InitializeAssets();

        return Task.CompletedTask;
    }

    void InitializePages()
    {
        _ = new GameEftPage();
    }

    void InitializeAssets()
    {
        //                              http://launcher.fuyu.api/* callback
        _contentService.SetOrAddLoader("assets/css/game-eft.css",  LoadContent);
        _contentService.SetOrAddLoader("assets/img/bg-eft.png",    LoadContent);
        _contentService.SetOrAddLoader("assets/img/logo-eft.png",  LoadContent);
    }

    Stream LoadContent(string path)
    {
        return path switch
        {
            // filepath                    stream
            "assets/css/game-eft.css"   => Resx.GetStream(Id, "assets.css.game-eft.css"),
            "assets/img/bg-eft.png"     => Resx.GetStream(Id, "assets.img.bg-eft.png"),
            "assets/img/logo-eft.png"   => Resx.GetStream(Id, "assets.img.logo-eft.png"),
            _                           => throw new FileNotFoundException()
        };
    }
}