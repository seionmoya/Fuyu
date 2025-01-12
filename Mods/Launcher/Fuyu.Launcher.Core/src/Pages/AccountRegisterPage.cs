using Fuyu.Common.Serialization;
using Fuyu.Launcher.Common.Models.Messages;
using Fuyu.Launcher.Common.Models.Pages;
using Fuyu.Launcher.Core.Models.Messages;
using Fuyu.Launcher.Core.Models.Replies;

namespace Fuyu.Launcher.Core.Pages;

public class AccountRegisterPage : AbstractPage
{
    protected override string Id { get; } = "Fuyu.Launcher.Core";
    protected override string Path { get; } = "account-register.html";

    protected override void HandleMessage(string message)
    {
        var data = Json.Parse<Message>(message);

        switch (data.Type)
        {
            case "LOADED_PAGE":
                OnLoadedPageMessage(message);
                return;

            case "REGISTER_CORE":
                OnAccountRegisterMessage(message);
                return;
        }
    }

    void OnLoadedPageMessage(string message)
    {
        // var body = Json.Parse<LoadedPageMessage>(message);
    }

    void OnAccountRegisterMessage(string message)
    {
        var data = Json.Parse<RegisterAccountMessage>(message);

        if (string.IsNullOrWhiteSpace(data.Username))
        {
            ReplyRegisterError();
            return;
        }

        if (string.IsNullOrWhiteSpace(data.Password))
        {
            ReplyRegisterError();
            return;
        }

        // TODO: Registration request
        // -- seionmoya, 2025-01-11

        // TODO: Registration validation
        // -- seionmoya, 2025-01-11

        // success! redirect
        ReplyRegisterSuccess();

        var page = "account-login.html";
        NavigationService.NavigateInternal(page);
    }

    void ReplyRegisterError()
    {
        var reply = new RegisterAccountReply
        {
            Type = "REGISTER_ERROR",
            Message = "Incorrect username or password."
        };

        var json = Json.Stringify(reply);
        MessageService.SendMessage(json);
    }

    void ReplyRegisterSuccess()
    {
        var reply = new RegisterAccountReply
        {
            Type = "REGISTER_SUCCESS",
            Message = "Success! You can now login."
        };

        var json = Json.Stringify(reply);
        MessageService.SendMessage(json);
    }
}