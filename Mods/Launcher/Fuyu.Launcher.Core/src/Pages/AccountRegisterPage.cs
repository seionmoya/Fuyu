using System;
using Fuyu.Backend.Core.Models.Responses;
using Fuyu.Common.Serialization;
using Fuyu.Launcher.Common.Models.Messages;
using Fuyu.Launcher.Common.Models.Pages;
using Fuyu.Launcher.Common.Services;
using Fuyu.Launcher.Core.Models.Accounts;
using Fuyu.Launcher.Core.Models.Messages;
using Fuyu.Launcher.Core.Models.Replies;
using Fuyu.Launcher.Core.Models.Requests;
using Fuyu.Launcher.Core.Models.Responses;

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
            SendRegisterErrorReply("Username is empty");
            return;
        }

        if (string.IsNullOrWhiteSpace(data.Password))
        {
            SendRegisterErrorReply("Password is empty");
            return;
        }

        var request = new AccountRegisterRequest()
        {
            Username = data.Username,
            Password = data.Password
        };
        var response = RequestService.Instance.Post<AccountRegisterResponse>("core", "/account/register", request);

        if (response.Status == ERegisterStatus.Success)
        {
            SendRegisterSuccessReply();

            var page = "account-login.html";
            NavigationService.NavigateInternal(page);
        }
        else
        {
            var errorMessage = response.Status switch
            {
                ERegisterStatus.UsernameEmpty => "No username provided.",
                ERegisterStatus.UsernameTooShort => "Username is too short.",
                ERegisterStatus.UsernameTooLong => "Username is too long.",
                ERegisterStatus.UsernameInvalidCharacter => "Username contains invalid characters.",
                ERegisterStatus.PasswordEmpty => "No password provided.",
                ERegisterStatus.PasswordTooShort => "Password is too short.",
                ERegisterStatus.PasswordTooLong => "Password is too long.",
                ERegisterStatus.PasswordMissingLowerCase => "Password doesn't contain lower-case characters.",
                ERegisterStatus.PasswordMissingUpperCase => "Password doesn't contain upper-case characters.",
                ERegisterStatus.PasswordMissingDigit => "Password doesn't contain digits.",
                ERegisterStatus.PasswordMissingSpecial => "Password doesn't contain special characters.",
                ERegisterStatus.AlreadyExists => "Account already exists.",
                _ => throw new Exception($"{response.Status} is not being handled")
            };

            SendRegisterErrorReply(errorMessage);
        }
    }

    void SendRegisterErrorReply(string errorMessage)
    {
        var reply = new RegisterAccountReply
        {
            Type = "REGISTER_ERROR",
            Message = errorMessage
        };

        var json = Json.Stringify(reply);
        MessageService.SendMessage(json);
    }

    void SendRegisterSuccessReply()
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