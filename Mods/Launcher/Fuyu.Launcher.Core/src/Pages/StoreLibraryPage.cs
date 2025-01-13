using Fuyu.Common.Serialization;
using Fuyu.Launcher.Common.Models.Messages;
using Fuyu.Launcher.Common.Models.Pages;
using Fuyu.Launcher.Core.Models.Messages;

namespace Fuyu.Launcher.Core.Pages;

public class StoreLibraryPage : AbstractPage
{
    protected override string Id { get; } = "Fuyu.Launcher.Core";
    protected override string Path { get; } = "store-library.html";

    protected override void HandleMessage(string message)
    {
        var data = Json.Parse<Message>(message);

        switch (data.Type)
        {
            case "NAVIGATE_GAME":
                OnNavigateGameMessage(message);
                return;
        }
    }

    void OnNavigateGameMessage(string message)
    {
        var body = Json.Parse<NavigateGameMessage>(message);

        var page = body.Game switch
        {
            "eft" => "store-eft.html",
            _ => string.Empty,
        };

        NavigationService.NavigateInternal(page);
    }
}