using System.Threading.Tasks;
using Fuyu.DependencyInjection;
using Fuyu.Common.Serialization;
using Fuyu.Launcher.Common.Models.Messages;
using Fuyu.Launcher.Common.Services;
using Fuyu.Modding;

namespace Fuyu.Launcher.Core;

// TODO: refactor into pages
// -- seionmoya, 2024-01-11
public class Mod : AbstractMod
{
    public override string Id { get; } = "Fuyu.Launcher.Core";
    public override string Name { get; } = "Fuyu.Launcher.Core";
    public override string[] Dependencies { get; } = [];

    private ContentService _contentService;
    private MessageService _messageService;
    private NavigationService _navigationService;

    public override Task OnLoad(DependencyContainer container)
    {
        // resolve dependencies
        _contentService = ContentService.Instance;
        _messageService = MessageService.Instance;
        _navigationService = NavigationService.Instance;

        //                  mod folder  http://launcher.fuyu.api/*              res path (use '.' as separator)
        _contentService.Add(Id,         "assets/css/bootstrap.min.css",         "assets.css.bootstrap.min.css");
        _contentService.Add(Id,         "assets/css/styles.css",                "assets.css.styles.css");
        _contentService.Add(Id,         "assets/img/logo.png",                  "assets.img.logo.png");
        _contentService.Add(Id,         "assets/js/bootstrap.bundle.min.js",    "assets.js.bootstrap.bundle.min.js");
        
        //                  page                     msg handler
        _messageService.Add("index.html",            HandleIndexMessage);

        // register pages
        _ = new AccountLibraryPage();
        _ = new AccountLoginPage();
        _ = new AccountRegisterPage();
        _ = new GameEftPage();
        _ = new SettingsPage();
        _ = new StoreEftPage();
        _ = new StoreLibraryPage();

        return Task.CompletedTask;
    }

    #region index.html
    void HandleIndexMessage(string message)
    {
        var data = Json.Parse<Message>(message);

        switch (data.Type)
        {
            case "LOADED_PAGE":
                HandleIndexPageLoadedMessage(message);
                return;
        }
    }

    void HandleIndexPageLoadedMessage(string message)
    {
        // var body = Json.Parse<LoadedPageMessage>(message);
        var page = "account-login.html";
        _navigationService.NavigateInternal(page);
    }
    #endregion
}