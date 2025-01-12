using Fuyu.Common.Serialization;
using Fuyu.Launcher.Common.Models.Messages;
using Fuyu.Launcher.Common.Models.Pages;

namespace Fuyu.Launcher.Core.Pages;

public class IndexPage : AbstractPage
{
    protected override string Id { get; } = "Fuyu.Launcher";
    protected override string Path { get; } = "index.html";

    protected override void HandleMessage(string message)
    {
        var data = Json.Parse<Message>(message);

        switch (data.Type)
        {
            case "LOADED_PAGE":
                OnLoadedPageMessage(message);
                return;
        }
    }

    void OnLoadedPageMessage(string message)
    {
        // var body = Json.Parse<LoadedPageMessage>(message);
        var page = "account-login.html";
        NavigationService.NavigateInternal(page);
    }
}