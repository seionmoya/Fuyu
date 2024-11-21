using Fuyu.Backend.Common.Models.Requests;
using Fuyu.Backend.Common.Models.Responses;
using Fuyu.Backend.Core.Models.Requests;
using Fuyu.Backend.Core.Models.Responses;
using Fuyu.Common.Hashing;
using Fuyu.Launcher.Core.Helpers;
using Fuyu.Launcher.Core.Models;

namespace Fuyu.Launcher.Core.Services
{
    public static class RequestService
    {
        static RequestService()
        {}

        public static HttpResponse<object> Ping()
        {
            return HttpHelper.HttpReq<object, object>(
                EHttpMethod.GET,
                "fuyu",
                "/ping",
                null);
        }

        public static HttpResponse<AccountRegisterResponse> RegisterAccount(string username, string password)
        {
            var request = new AccountRegisterRequest()
            {
                Username = username,
                Password = password
            };

            return HttpHelper.HttpReq<AccountRegisterRequest, AccountRegisterResponse>(
                EHttpMethod.POST,
                "fuyu",
                "/account/register",
                request);
        }

        public static HttpResponse<AccountLoginResponse> LoginAccount(string username, string password)
        {
            var hashedPassword = Sha256.Generate(password);
            var request = new AccountLoginRequest()
            {
                Username = username,
                Password = hashedPassword
            };

            return HttpHelper.HttpReq<AccountLoginRequest, AccountLoginResponse>(
                EHttpMethod.POST,
                "fuyu",
                "/account/login",
                request);
        }

        public static void LogoutAccount()
        {
            HttpHelper.HttpReq<object, object>(
                EHttpMethod.PUT,
                "fuyu",
                "/account/logout",
                null);

            HttpHelper.ResetSessions();
        }

        public static HttpResponse<AccountGamesResponse> GetGames()
        {
            return HttpHelper.HttpReq<object, AccountGamesResponse>(
                EHttpMethod.POST,
                "fuyu",
                "/account/games",
                null);
        }

        public static HttpResponse<AccountRegisterGameResponse> RegisterGame(string game, string edition)
        {
            var request = new AccountRegisterGameRequest()
            {
                Game = game,
                Edition = edition
            };

            return HttpHelper.HttpReq<AccountRegisterGameRequest, AccountRegisterGameResponse>(
                EHttpMethod.POST,
                "fuyu",
                "/account/register/game",
                request);
        }

        public static HttpResponse<FuyuGameLoginResponse> LoginGame(string game, int accountId)
        {
            var request = new FuyuGameLoginRequest()
            {
                AccountId = accountId
            };

            return HttpHelper.HttpReq<FuyuGameLoginRequest, FuyuGameLoginResponse>(
                EHttpMethod.POST,
                game,
                "/fuyu/game/login",
                request);
        }
    }
}
