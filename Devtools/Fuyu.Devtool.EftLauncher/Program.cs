using Fuyu.Launcher.Core.Helpers;
using Fuyu.Launcher.Core.Services;

namespace Fuyu.Devtool.EftLauncher
{
    public class Program
    {
        static void Main()
        {
            // Register account
            var username = "User";
            var password = "Passw123!";
            RequestService.RegisterAccount(username, password);

            // Login account
            var coreBackendUrl = "http://localhost:8000";
            var loginAccountResponse = RequestService.LoginAccount(username, password);
            var coreSessionId = loginAccountResponse.Response.SessionId;
            HttpHelper.CreateSession("fuyu", coreBackendUrl, coreSessionId);

            // Register game
            var game = "eft";
            var edition = "unheard";
            RequestService.RegisterGame(game, edition);

            // Get game
            var getGamesResponse = RequestService.GetGames();
            var gameAccountId = getGamesResponse.Response.Games[game].Value;

            // Login game
            var loginGameResponse = RequestService.LoginGame(game, gameAccountId);
            var eftSessionId = loginGameResponse.Response.SessionId;

            // Start EFT
            var eftBackendUrl = "http://localhost:8010";
            var process = ProcessService.StartEft(string.Empty, eftSessionId, eftBackendUrl);
            process.Start();
        }
    }
}