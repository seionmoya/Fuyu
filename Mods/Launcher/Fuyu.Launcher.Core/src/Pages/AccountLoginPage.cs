using Fuyu.Common.Serialization;
using Fuyu.Launcher.Common.Models.Messages;
using Fuyu.Launcher.Common.Models.Pages;
using Fuyu.Launcher.Core.Models.Messages;
using Fuyu.Launcher.Core.Models.Replies;

namespace Fuyu.Launcher.Core.Pages;

public class AccountLoginPage : AbstractPage
{
    protected override string Id { get; } = "Fuyu.Launcher.Core";
    protected override string Path { get; } = "account-login.html";

    protected override void HandleMessage(string message)
    {
        var data = Json.Parse<Message>(message);

        switch (data.Type)
        {
            case "LOADED_PAGE":
                OnLoadedPageMessage(message);
                return;

            case "LOGIN_CORE":
                OnLoginAccountMessage(message);
                return;
        }
    }

    void OnLoadedPageMessage(string message)
    {
        // var body = Json.Parse<LoadedPageMessage>(message);
    }

    void OnLoginAccountMessage(string message)
    {
        var body = Json.Parse<LoginAccountMessage>(message);

        if (string.IsNullOrWhiteSpace(body.Username))
        {
            ReplyLoginError();
            return;
        }

        if (string.IsNullOrWhiteSpace(body.Password))
        {
            ReplyLoginError();
            return;
        }

        // TODO: Login request
        // -- seionmoya, 2025-01-11

        // TODO: Login validation
        // -- seionmoya, 2025-01-11

        // success! redirect
        var page = "account-library.html";
        NavigationService.NavigateInternal(page);
    }

    void ReplyLoginError()
    {
        var reply = new LoginAccountReply
        {
            Type = "LOGIN_ERROR",
            Message = "Incorrect username or password."
        };

        var json = Json.Stringify(reply);
        MessageService.SendMessage(json);
    }
}