using System.IO;
using System.Threading.Tasks;
using Fuyu.Common.IO;
using Fuyu.Common.Networking;
using Fuyu.Common.Services;
using Fuyu.DependencyInjection;
using Fuyu.Launcher.Common.Services;
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

    public override Task OnLoad(DependencyContainer container)
    {
        // resolve dependencies
        _contentService = ContentService.Instance;

        Resx.SetSource(Id, this.GetType().Assembly);

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
            "assets/css/game-eft.css"           => Resx.GetStream(Id, "assets.css.game-eft.css"),
            "assets/img/bg-eft.png"             => Resx.GetStream(Id, "assets.img.bg-eft.png"),
            "assets/img/logo-eft.png"           => Resx.GetStream(Id, "assets.img.logo-eft.png"),
            _                                   => throw new FileNotFoundException()
        };
    }
}