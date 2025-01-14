using System.Diagnostics;
using Fuyu.Common.Serialization;
using Fuyu.Common.Launcher.Models.Messages;
using Fuyu.Common.Launcher.Models.Pages;
using Fuyu.Launcher.EFT.Models.Configs;
using Fuyu.Launcher.EFT.Models.Messages;
using Fuyu.Launcher.EFT.Models.Replies;
using Fuyu.Launcher.EFT.Models.Responses;
using Fuyu.Launcher.EFT.Models.Requests;

namespace Fuyu.Launcher.EFT.Pages;

public class GameEftPage : AbstractPage
{
    protected override string Id { get; } = "Fuyu.Launcher.EFT";
    protected override string Path { get; } = "game-eft.html";

    private readonly string _eftPath;

    public GameEftPage() : base()
    {
        _eftPath = @"C:\Games\EFT-Live";
    }

    protected override void HandleMessage(string message)
    {
        var data = Json.Parse<Message>(message);

        switch (data.Type)
        {
            case "LAUNCH_GAME":
                OnGameLaunchMessage(message);
                return;
        }
    }

    void OnGameLaunchMessage(string message)
    {
        var body = Json.Parse<LaunchGameMessage>(message);

        // request sessionId
        var accountId = RequestGameAccountId("eft", "unheard");
        var gameSessionId = RequestSessionId(accountId);

        // TODO: Keep track of game lifecycle
        // -- seionmoya, 2025-01-11
        var process = GetEftProcess(_eftPath, "http://localhost:8010/", gameSessionId);
        process.Start();

        ReplyLaunchSuccess();
    }

    void ReplyLaunchSuccess()
    {
        var reply = new LaunchGameReply
        {
            Type = "LAUNCH_SUCCESS",
            Message = string.Empty
        };

        var json = Json.Stringify(reply);
        MessageService.SendMessage(json);
    }

    int RequestGameAccountId(string game, string edition)
    {
        var account = RequestService.Get<AccountGetResponse>("core", "/account/get");

        if (account.Games.TryGetValue(game, out int? gameAccountId) && gameAccountId.HasValue)
        {
            // find existing game account
            return gameAccountId.Value;
        }
        else
        {
            // register game
            var request = new AccountRegisterGameRequest()
            {
                Game = game,
                Edition = edition
            };
            var response = RequestService.Post<AccountRegisterGameResponse>("core", "/account/register/game", request);

            return response.AccountId;
        }
    }

    string RequestSessionId(int accountId)
    {
        var request = new FuyuGameLoginRequest()
        {
            AccountId = accountId
        };
        var response = RequestService.Post<FuyuGameLoginResponse>("eft", "/fuyu/game/login", request);

        var sessionId = response.SessionId;
        return sessionId;
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