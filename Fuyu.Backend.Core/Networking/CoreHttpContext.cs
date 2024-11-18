using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Fuyu.Common.Networking;

namespace Fuyu.Backend.Core.Networking
{
    public class CoreHttpContext : HttpContext
    {
        public CoreHttpContext(HttpListenerRequest request, HttpListenerResponse response) : base(request, response)
        {
        }

        public override byte[] GetBinary()
        {
            using (var ms = new MemoryStream())
            {
                Request.InputStream.CopyTo(ms);

                var body = ms.ToArray();
                var encryption = GetEncryption();

                if (encryption != string.Empty)
                {
                    switch (encryption)
                    {
                        case "aes":
                            // TODO: handle AES-192 encryption
                            // body = CryptographyService.DecryptAes(body);
                            break;

                        default:
                            throw new InvalidDataException(encryption);
                    }
                }

                return body;
            }
        }

        protected Task SendAsync(byte[] data, string mime, HttpStatusCode status, bool encrypted)
        {
            var hasData = !(data == null);

            // Used for postman debugging by Nexus4880
            // -- seionmoya, 2024-11-18
#if DEBUG
            if (Request.Headers["X-Require-Plaintext"] != null)
            {
                encrypted = false;
            }
#endif

            if (hasData && encrypted)
            {
                // TODO: handle X-Encryption: aes
                /*
                Response.Headers.Add("X-Encryption", "aes");
                data = CryptographyService.EncryptAes(data);
                */
                encrypted = false;
            }

            return SendAsync(data, mime, status);
        }

        public Task SendBinaryAsync(byte[] data, string mime, bool encrypted)
        {
            return SendAsync(data, mime, HttpStatusCode.OK, encrypted);
        }

        public Task SendJsonAsync(string text, bool encrypted)
        {
            var encoded = Encoding.UTF8.GetBytes(text);
            var mime = encrypted
                ? "application/octet-stream"
                : "application/json; charset=utf-8";

            return SendAsync(encoded, mime, HttpStatusCode.OK, encrypted);
        }

        public string GetEncryption()
        {
            return Request.Headers["X-Encryption"];
        }

        public string GetSessionId()
        {
            return Request.Cookies["Session"].Value;
        }
    }
}