using Fuyu.Common.Serialization;
using Fuyu.Launcher.Common.Models.Messages;
using Fuyu.Launcher.Common.Models.Pages;

namespace Fuyu.Launcher.Core.Pages;

public class SettingsPage : AbstractPage
{
    protected override string Id { get; } = "Fuyu.Launcher.Core";
    protected override string Path { get; } = "settings.html";

    protected override void HandleMessage(string message)
    {
        var data = Json.Parse<Message>(message);

        switch (data.Type)
        {
            case "LOADED_PAGE":
                OnLoadedPageMessage(message);
                return;

            case "SAVE_SETTINGS":
                OnSaveSettingsMessage(message);
                return;
        }
    }

    void OnLoadedPageMessage(string message)
    {
        // var body = Json.Parse<LoadedPageMessage>(message);
    }

    void OnSaveSettingsMessage(string message)
    {
        //var body = Json.Parse<SaveSettingsMessage>(message);
        NavigationService.NavigatePrevious();
    }
}