using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Fuyu.DependencyInjection;
using Fuyu.Common.Serialization;
using Fuyu.Launcher.Common.Models.Messages;
using Fuyu.Launcher.Common.Services;
using Fuyu.Launcher.Core.Models.Configs;
using Fuyu.Launcher.Core.Models.Messages;
using Fuyu.Launcher.Core.Models.Replies;
using Fuyu.Modding;

namespace Fuyu.Launcher.Core;

// TODO: refactor into pages
// -- seionmoya, 2024-01-11
public class Mod : AbstractMod
{
    public override string Id { get; } = "Fuyu.Launcher.Core";
    public override string Name { get; } = "Fuyu.Launcher.Core";
    public override string[] Dependencies { get; } = [];

    private ContentService _contentService;
    private MessageService _messageService;
    private NavigationService _navigationService;

    private string _eftPath;
    private string _coreSessionId;
    private string _eftSessionId;

    public Mod()
    {
        _eftPath = string.Empty;
        _eftSessionId = string.Empty;
    }

    public override Task OnLoad(DependencyContainer container)
    {
        _contentService = ContentService.Instance;
        _messageService = MessageService.Instance;
        _navigationService = NavigationService.Instance;

        RegisterContent();   
        RegisterMessaging();

        return Task.CompletedTask;
    }

    void RegisterContent()
    {
        //                  mod folder   http://launcher.fuyu.api/*            res path (use '.' as separator)
        _contentService.Add(Id,          "account-library.html",               "account-library.html");
        _contentService.Add(Id,          "account-login.html",                 "account-login.html");
        _contentService.Add(Id,          "account-register.html",              "account-register.html");
        _contentService.Add(Id,          "game-eft.html",                      "game-eft.html");
        _contentService.Add(Id,          "store-library.html",                 "store-library.html");
        _contentService.Add(Id,          "store-eft.html",                     "store-eft.html");
        _contentService.Add(Id,          "settings.html",                      "settings.html");
        _contentService.Add(Id,          "assets/css/bootstrap.min.css",       "assets.css.bootstrap.min.css");
        _contentService.Add(Id,          "assets/css/styles.css",              "assets.css.styles.css");
        _contentService.Add(Id,          "assets/img/logo.png",                "assets.img.logo.png");
        _contentService.Add(Id,          "assets/js/bootstrap.bundle.min.js",  "assets.js.bootstrap.bundle.min.js");
    }

    void RegisterMessaging()
    {
        //                  page                     msg handler
        _messageService.Add("index.html",            HandleIndexMessage);
        _messageService.Add("account-library.html",  HandleAccountLibraryMessage);
        _messageService.Add("account-login.html",    HandleAccountLoginMessage);
        _messageService.Add("account-register.html", HandleAccountRegisterMessage);
        _messageService.Add("game-eft.html",         HandleGameEftMessage);
        _messageService.Add("store-library.html",    HandleStoreLibraryMessage);
        _messageService.Add("store-eft.html",        HandleStoreEftMessage);
        _messageService.Add("settings.html",         HandleSettingsMessage);
    }

    void HandleIndexMessage(string message)
    {
        var data = Json.Parse<LoadedPageMessage>(message);

        if (data.Type == "LOADED_PAGE")
        {
            var page = "account-login.html";
            _navigationService.NavigateInternal(page);
            return;
        }
    }

    void HandleAccountLibraryMessage(string message)
    {
        var data = Json.Parse<ViewAccountGameMessage>(message);

        if (data.Type == "NAVIGATE_GAME")
        {
            // TODO: navigation system
            // -- seionmoya, 2025-01-10
            switch (data.Game)
            {
                case "eft":
                    var page = "game-eft.html";
                    _navigationService.NavigateInternal(page);
                    return;

                default:
                    throw new Exception($"{data.Game} is not supported");
            }
        }
    }

    void HandleAccountLoginMessage(string message)
    {
        var data = Json.Parse<LoginAccountMessage>(message);

        // send HTTP request to backend

        if (data.Type == "LOGIN_CORE")
        {
            if (string.IsNullOrWhiteSpace(data.Username))
            {
                ReplyLoginError();
                return;
            }

            if (string.IsNullOrWhiteSpace(data.Password))
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
            _navigationService.NavigateInternal(page);
        }
    }

    void ReplyLoginError()
    {
        var reply = new LoginAccountReply
        {
            Type = "LOGIN_ERROR",
            Message = "Incorrect username or password."
        };

        var json = Json.Stringify(reply);
        _messageService.SendMessage(json);
    }

    void HandleAccountRegisterMessage(string message)
    {
        var data = Json.Parse<RegisterAccountMessage>(message);

        // send HTTP request to backend

        if (data.Type == "REGISTER_CORE")
        {
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
            _navigationService.NavigateInternal(page);
        }
    }

    void ReplyRegisterError()
    {
        var reply = new RegisterAccountReply
        {
            Type = "REGISTER_ERROR",
            Message = "Incorrect username or password."
        };

        var json = Json.Stringify(reply);
        _messageService.SendMessage(json);
    }

    void ReplyRegisterSuccess()
    {
        var reply = new RegisterAccountReply
        {
            Type = "REGISTER_SUCCESS",
            Message = "Success! You can now login."
        };

        var json = Json.Stringify(reply);
        _messageService.SendMessage(json);
    }

    void HandleGameEftMessage(string message)
    {
        var data = Json.Parse<RegisterAccountMessage>(message);

        if (data.Type == "LAUNCH_GAME")
        {
            // TODO: Keep track of game lifecycle
            // -- seionmoya, 2025-01-11
            var process = GetEftProcess(_eftPath, "http://localhost:8010/", _eftSessionId);
            process.Start();

            ReplyLaunchSuccess();
        }
    }

    void ReplyLaunchSuccess()
    {
        var reply = new RegisterAccountReply
        {
            Type = "LAUNCH_SUCCESS",
            Message = string.Empty
        };

        var json = Json.Stringify(reply);
        _messageService.SendMessage(json);
    }

    void HandleStoreLibraryMessage(string message)
    {
        var data = Json.Parse<NavigateGameMessage>(message);

        if (data.Type == "NAVIGATE_GAME")
        {
            var page = data.Game switch
            {
                "eft" => "store-eft.html",
                _ => string.Empty,
            };

            _navigationService.NavigateInternal(page);
            return;
        }
    }

    void HandleStoreEftMessage(string message)
    {
        var data = Json.Parse<NavigateGameMessage>(message);

        if (data.Type == "ADD_GAME")
        {
            // TODO: add game
            // -- seionmoya, 2025-01-10
            return;
        }
    }

    void HandleSettingsMessage(string message)
    {
        var data = Json.Parse<SaveSettingsMessage>(message);

        if (data.Type == "SAVE_SETTINGS")
        {
            // TODO: run settings save callbacks
            // -- seionmoya, 2025-01-10

            _navigationService.NavigatePrevious();
            return;
        }
    }

    Process GetEftProcess(string cwd, string sessionId, string address)
    {
        // set filepath
        var processStartInfo = new ProcessStartInfo()
        {
            FileName = $"{cwd}/EscapeFromTarkov.exe",
            WorkingDirectory = cwd
        };

        // add token
        processStartInfo.ArgumentList.Add($"-token={sessionId}");

        // add eft startup config
        var config = new EFTStartupConfig()
        {
            BackendUrl = address,
            Version = "live",
            MatchingVersion = "live"
        };
        var json = Json.Stringify(config);

        processStartInfo.ArgumentList.Add(json);        

        // create process
        return new Process()
        {
            StartInfo = processStartInfo
        };
    }
}