using System;
using Fuyu.Common.Serialization;
using Fuyu.Launcher.Common.Models.Messages;
using Fuyu.Launcher.Common.Models.Pages;
using Fuyu.Launcher.Core.Models.Messages;

namespace Fuyu.Launcher.Core.Pages;

public class AccountLibraryPage : AbstractPage
{
    protected override string Id { get; } = "Fuyu.Launcher.Core";
    protected override string Path { get; } = "account-library.html";

    protected override void HandleMessage(string message)
    {
        var data = Json.Parse<Message>(message);

        switch (data.Type)
        {
            case "LOADED_PAGE":
                OnLoadedPageMessage(message);
                return;

            case "NAVIGATE_GAME":
                OnViewAccountGameMessage(message);
                return;
        }
    }

    void OnLoadedPageMessage(string message)
    {
        // var body = Json.Parse<LoadedPageMessage>(message);
    }

    void OnViewAccountGameMessage(string message)
    {
        var body = Json.Parse<ViewAccountGameMessage>(message);

        // TODO: GameService navigation system
        // -- seionmoya, 2025-01-10
        switch (body.Game)
        {
            case "eft":
                var page = "game-eft.html";
                NavigationService.NavigateInternal(page);
                return;

            default:
                throw new Exception($"{body.Game} is not supported");
        }
    }
}