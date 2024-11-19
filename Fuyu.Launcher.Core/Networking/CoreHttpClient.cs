using System;
using System.IO.Compression;
using System.Net.Http;

namespace Fuyu.Launcher.Core.Networking
{
    public class CoreHttpClient : Fuyu.Common.Networking.HttpClient
    {
        public readonly string _sessionId;

        public CoreHttpClient(string address, string sessionId) : base(address)
        {
            _sessionId = sessionId;
        }

        protected override HttpRequestMessage GetNewRequest(HttpMethod method, string path)
        {
            var request = new HttpRequestMessage()
            {
                Method = method,
                RequestUri = new Uri(Address + path),
            };

            request.Headers.Add("X-Encryption", "aes");
            request.Headers.Add("Cookie", $"Session={_sessionId}");

            return request;
        }
    }
}