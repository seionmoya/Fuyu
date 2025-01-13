using System.Diagnostics;
using Fuyu.Common.Serialization;
using Fuyu.Launcher.Common.Models.Messages;
using Fuyu.Launcher.Common.Models.Pages;
using Fuyu.Launcher.Core.Models.Configs;
using Fuyu.Launcher.Core.Models.Replies;

namespace Fuyu.Launcher.Core.Pages;

public class GameEftPage : AbstractPage
{
    protected override string Id { get; } = "Fuyu.Launcher.Core";
    protected override string Path { get; } = "game-eft.html";

    private string _eftPath;
    private string _eftSessionId;

    public GameEftPage() : base()
    {
        _eftPath = string.Empty;
        _eftSessionId = string.Empty;
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

        // TODO: Keep track of game lifecycle
        // -- seionmoya, 2025-01-11
        var process = GetEftProcess(_eftPath, "http://localhost:8010/", _eftSessionId);
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