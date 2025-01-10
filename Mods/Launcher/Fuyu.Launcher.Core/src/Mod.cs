using System.Threading.Tasks;
using Fuyu.DependencyInjection;
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

    public override Task OnLoad(DependencyContainer container)
    {
        RegisterContent();   
        RegisterMessaging();
        return Task.CompletedTask;
    }

    void RegisterContent()
    {
        //                 mod folder   http://launcher.fuyu.api/*            res path (use '.' as separator)
        ContentService.Add(Id,          "account-library.html",               "account-library.html");
        ContentService.Add(Id,          "account-login.html",                 "account-login.html");
        ContentService.Add(Id,          "account-register.html",              "account-register.html");
        ContentService.Add(Id,          "game-eft.html",                      "game-eft.html");
        ContentService.Add(Id,          "store-library.html",                 "store-library.html");
        ContentService.Add(Id,          "store-eft.html",                     "store-eft.html");
        ContentService.Add(Id,          "settings.html",                      "settings.html");
        ContentService.Add(Id,          "assets/css/bootstrap.min.css",       "assets.css.bootstrap.min.css");
        ContentService.Add(Id,          "assets/css/styles.css",              "assets.css.styles.css");
        ContentService.Add(Id,          "assets/img/logo.png",                "assets.img.logo.png");
        ContentService.Add(Id,          "assets/js/bootstrap.bundle.min.js",  "assets.js.bootstrap.bundle.min.js");
    }

    void RegisterMessaging()
    {
        //                 page                     msg handler
        MessageService.Add("index.html",            HandleIndexMessage);
        MessageService.Add("account-library.html",  HandleAccountLibraryMessage);
        MessageService.Add("account-login.html",    HandleAccountLoginMessage);
        MessageService.Add("account-register.html", HandleAccountRegisterMessage);
        MessageService.Add("game-eft.html",         HandleGameEftMessage);
        MessageService.Add("store-library.html",    HandleStoreLibraryMessage);
        MessageService.Add("store-eft.html",        HandleStoreEftMessage);
        MessageService.Add("settings.html",         HandleSettingsMessage);
    }

    void NavigateInternal(string page)
    {
        var url = NavigationService.GetInternalUrl(page);
        NavigationService.Navigate(url);
    }

    void HandleIndexMessage(string message)
    {
        if (message == "LOADING_COMPLETED")
        {
            var page = "account-login.html";
            NavigateInternal(page);
            return;
        }
    }

    void HandleAccountLibraryMessage(string message)
    {
        if (!string.IsNullOrWhiteSpace(message))
        {
            // TODO: navigation system
            // -- seionmoya, 2025-01-10
            var page = "game-eft.html";
            NavigateInternal(page);
            return;
        }
    }

    void HandleAccountLoginMessage(string message)
    {
        if (!string.IsNullOrWhiteSpace(message))
        {
            // TODO: Login validation
            // -- seionmoya, 2025-01-10
            var page = "account-library.html";
            NavigateInternal(page);
            return;
        }
    }

    void HandleAccountRegisterMessage(string message)
    {
        if (!string.IsNullOrWhiteSpace(message))
        {
            // TODO: Registration validation
            // -- seionmoya, 2025-01-10
            var page = "account-login.html";
            NavigateInternal(page);
            return;
        }
    }

    void HandleGameEftMessage(string message)
    {
        if (!string.IsNullOrWhiteSpace(message))
        {
            // TODO: Launch game callback
            // -- seionmoya, 2025-01-10
            return;
        }
    }

    void HandleStoreLibraryMessage(string message)
    {
        if (!string.IsNullOrWhiteSpace(message))
        {
            // TODO: navigation system
            // -- seionmoya, 2025-01-10
            var page = "store-eft.html";
            NavigateInternal(page);
            return;
        }
    }

    void HandleStoreEftMessage(string message)
    {
        if (!string.IsNullOrWhiteSpace(message))
        {
            // TODO: add game
            // -- seionmoya, 2025-01-10
            return;
        }
    }

    void HandleSettingsMessage(string message)
    {
        if (message == "NAVIGATE_BACK")
        {
            NavigationService.NavigatePrevious();
            return;
        }
    }
}