using System;
using System.IO.Compression;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Text;
using Fuyu.Common.Compression;
using Fuyu.Common.Serialization;
using Fuyu.Launcher.Core.Helpers;
using Fuyu.Launcher.Core.Models;
using Fuyu.Launcher.Core.Services;

namespace Fuyu.Devtool.EftLauncher
{
    [DataContract]
    public class GameProfileCreateRequest
    {
        [DataMember(Name = "side")]
        public string Side { get; set; }

        [DataMember(Name = "nickname")]
        public string Nickname { get; set; }

        [DataMember(Name = "headId")]
        public string HeadId { get; set; }

        [DataMember(Name = "voiceId")]
        public string VoiceId { get; set; }
    }

        public class EftHttpClient : Fuyu.Common.Networking.HttpClient
    {
        public readonly string _sessionId;

        public EftHttpClient(string address, string sessionId) : base(address)
        {
            _sessionId = sessionId;
        }

        protected override byte[] OnSendBody(byte[] body)
        {
            return MemoryZlib.Compress(body, CompressionLevel.SmallestSize);
        }

        protected override byte[] OnReceiveBody(byte[] body)
        {
            if (MemoryZlib.IsCompressed(body))
            {
                body = MemoryZlib.Decompress(body);
            }

            return body;
        }

        protected override HttpRequestMessage GetNewRequest(HttpMethod method, string path)
        {
            var request = new HttpRequestMessage()
            {
                Method = method,
                RequestUri = new Uri(Address + path),
            };

            request.Headers.Add("X-Encryption", "aes");
            request.Headers.Add("Cookie", $"PHPSESSID={_sessionId}");

            return request;
        }
    }

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

            // Set core session
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

            // Set EFT session
            var eftBackendUrl = "http://localhost:8010";
            HttpHelper.CreateSession("eft", eftBackendUrl, eftSessionId);

            // Create EFT profile
            var data = new GameProfileCreateRequest()
            {
                Side = "Usec",
                Nickname = username,
                HeadId = "5cde96047d6c8b20b577f016",
                VoiceId = "6284d6ab8e4092597733b7a7"
            };
            var json = Json.Stringify(data);
            var bytes = Encoding.UTF8.GetBytes(json);
            var eftHttpClient = new EftHttpClient(eftBackendUrl, eftSessionId);
            eftHttpClient.Put("/client/game/profile/create", bytes);

            // Start EFT
            var process = ProcessService.StartEft(string.Empty, eftSessionId, eftBackendUrl);
            process.Start();
        }
    }
}