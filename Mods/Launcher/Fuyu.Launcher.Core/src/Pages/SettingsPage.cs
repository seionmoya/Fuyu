using Fuyu.Common.Serialization;
using Fuyu.Common.Launcher.Models.Messages;
using Fuyu.Common.Launcher.Models.Pages;

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
            case "SAVE_SETTINGS":
                OnSaveSettingsMessage(message);
                return;
        }
    }

    void OnSaveSettingsMessage(string message)
    {
        //var body = Json.Parse<SaveSettingsMessage>(message);
        NavigationService.NavigatePrevious();
    }
}