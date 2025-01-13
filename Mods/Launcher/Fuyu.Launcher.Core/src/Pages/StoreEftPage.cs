using Fuyu.Common.Serialization;
using Fuyu.Launcher.Common.Models.Messages;
using Fuyu.Launcher.Common.Models.Pages;

namespace Fuyu.Launcher.Core.Pages;

public class StoreEftPage : AbstractPage
{
    protected override string Id { get; } = "Fuyu.Launcher.Core";
    protected override string Path { get; } = "store-eft.html";

    protected override void HandleMessage(string message)
    {
        var data = Json.Parse<Message>(message);

        switch (data.Type)
        {
            case "ADD_GAME":
                OnAddGameMessage(message);
                return;
        }
    }

    void OnAddGameMessage(string message)
    {
        // var body = Json.Parse<Message>(message);
        // do something
    }
}