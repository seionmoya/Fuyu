using System.Threading.Tasks;
using Fuyu.DependencyInjection;
using Fuyu.Launcher.Common.Services;
using Fuyu.Modding;

namespace Fuyu.Launcher.Core;

public class Mod : AbstractMod
{
    public override string Id { get; } = "Fuyu.Launcher.Core";
    public override string Name { get; } = "Fuyu.Launcher.Core";
    public override string[] Dependencies { get; } = [];

    public override Task OnLoad(DependencyContainer container)
    {
        RegisterContent();   
        RegisterMessaging();
        return Task.CompletedTask;
    }

    void RegisterContent()
    {
        //                 mod folder   http://launcher.fuyu.api/*            res path (use '.' as separator)
        ContentService.Add(Id,          "account-game.html",                  "account-game.html");
        ContentService.Add(Id,          "account-library.html",               "account-library.html");
        ContentService.Add(Id,          "account-login.html",                 "account-login.html");
        ContentService.Add(Id,          "account-register.html",              "account-register.html");
        ContentService.Add(Id,          "game-library.html",                  "game-library.html");
        ContentService.Add(Id,          "game-overview.html",                 "game-overview.html");
        ContentService.Add(Id,          "settings.html",                      "settings.html");
        ContentService.Add(Id,          "assets/css/bootstrap.min.css",       "assets.css.bootstrap.min.css");
        ContentService.Add(Id,          "assets/css/styles.css",              "assets.css.styles.css");
        ContentService.Add(Id,          "assets/js/bootstrap.bundle.min.js",  "assets.js.bootstrap.bundle.min.js");
    }

    void RegisterMessaging()
    {
        //                 page                     msg handler
        MessageService.Add("index.html",            HandleIndexMessage);
        MessageService.Add("account-game.html",     HandleNull);
        MessageService.Add("account-library.html",  HandleNull);
        MessageService.Add("account-login.html",    HandleNull);
        MessageService.Add("account-register.html", HandleNull);
        MessageService.Add("game-library.html",     HandleNull);
        MessageService.Add("game-overview.html",    HandleNull);
        MessageService.Add("settings.html",         HandleNull);
    }

    void HandleNull(string message)
    {
        // intentionally empty
    }

    void HandleIndexMessage(string message)
    {
        if (message == "LOADING_COMPLETED")
        {
            var url = NavigationService.GetInternalUrl("account-login.html");
            NavigationService.Navigate(url);
            return;
        }
    }
}