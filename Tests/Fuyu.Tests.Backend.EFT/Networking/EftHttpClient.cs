using System;
using System.IO.Compression;
using System.Net.Http;
using Fuyu.Common.Compression;

namespace Fuyu.Tests.Backend.EFT.Networking
{
    public class EftHttpClient : Fuyu.Common.Networking.HttpClient
    {
        public readonly string _sessionId;

        public EftHttpClient(string address, string sessionId) : base(address)
        {
            _sessionId = sessionId;
        }

        protected override byte[] OnSendBody(byte[] body)
        {
            // NOTE: CompressionLevel.SmallestSize does not exist in
            //       .NET 5 and below.
            // -- seionmoya, 2024-10-07
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
}