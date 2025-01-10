using System.Threading.Tasks;
using Fuyu.Common.IO;
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

        // done loading the mod!
        return Task.CompletedTask;
    }

    void RegisterContent()
    {
        //                 mod folder   http://launcher.fuyu.api/*      res path (use '.' as separator)
        ContentService.Add(Id,          "favicon.ico",                  "assets.img.favicon.ico");
        ContentService.Add(Id,          "index.html",                   "index.html");
        ContentService.Add(Id,          "msg.html",                     "msg.html");
        ContentService.Add(Id,          "assets/css/bootstrap.min.css", "assets.css.bootstrap.min.css");
        ContentService.Add(Id,          "assets/css/styles.css",        "assets.css.styles.css");
        ContentService.Add(Id,          "assets/img/bg-eft.png",        "assets.img.bg-eft.png");
    }

    void RegisterMessaging()
    {
        //                 page        msg handler
        MessageService.Add("msg.html", HandleMessage);
    }

    void HandleMessage(string message)
    {
        // --- EXAMPLE: get current page
        var url = NavigationService.CurrentPage;
        Terminal.WriteLine($"The current page url is: {url}");

        // --- EXAMPLE: redirect
        if (message == "NAVIGATE_HOME")
        {
            var newUrl = NavigationService.GetInternalUrl("index.html");
            NavigationService.Navigate(newUrl);
            return;
        }

        // --- EXAMPLE: replying
        var reply = string.Empty;

        if (message == "START_EFT")
        {
            reply = "Don't think so!";
        }

        MessageService.SendMessage(reply);
    }
}