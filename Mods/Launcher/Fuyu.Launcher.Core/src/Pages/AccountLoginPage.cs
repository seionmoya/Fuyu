using System;
using Fuyu.Backend.Core.Models.Responses;
using Fuyu.Common.Hashing;
using Fuyu.Common.Serialization;
using Fuyu.Launcher.Common.Models.Messages;
using Fuyu.Launcher.Common.Models.Pages;
using Fuyu.Launcher.Common.Services;
using Fuyu.Launcher.Core.Models.Accounts;
using Fuyu.Launcher.Core.Models.Messages;
using Fuyu.Launcher.Core.Models.Replies;
using Fuyu.Launcher.Core.Models.Requests;
using Fuyu.Launcher.Core.Networking;

namespace Fuyu.Launcher.Core.Pages;

public class AccountLoginPage : AbstractPage
{
    protected override string Id { get; } = "Fuyu.Launcher.Core";
    protected override string Path { get; } = "account-login.html";

    private readonly RequestService _requestService;

    public AccountLoginPage() : base()
    {
        _requestService = RequestService.Instance;
    }

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

        // validate forms
        if (string.IsNullOrWhiteSpace(body.Username))
        {
            SendLoginErrorReply("Empty username");
            return;
        }

        if (string.IsNullOrWhiteSpace(body.Password))
        {
            SendLoginErrorReply("Empty password");
            return;
        }

        // send request
        var hashedPassword = Sha256.Generate(body.Password);
        var request = new AccountLoginRequest()
        {
            Username = body.Username,
            Password = hashedPassword
        };
        var response = _requestService.Post<AccountLoginResponse>("core", "/account/login", request);

        // handle response
        if (response.Status == ELoginStatus.Success)
        {
            var coreClient = new CoreHttpClient("http://localhost:8000", response.SessionId);
            _requestService.AddOrSetClient("core", coreClient);

            var page = "account-library.html";
            NavigationService.NavigateInternal(page);
        }
        else
        {
            var errorMessage = response.Status switch
            {
                ELoginStatus.AccountBanned => "Account is banned.",
                ELoginStatus.AccountNotFound => "Account is not found.",
                ELoginStatus.SessionAlreadyExists => "Account is already logged in.",
                _ => throw new Exception("Unhandled case")
            };

            SendLoginErrorReply(errorMessage);
        }
    }

    void SendLoginErrorReply(string errorMessage)
    {
        var reply = new LoginAccountReply
        {
            Type = "LOGIN_ERROR",
            Message = errorMessage
        };

        var json = Json.Stringify(reply);
        MessageService.SendMessage(json);
    }
}