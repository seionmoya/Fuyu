using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Fuyu.Common.Compression;
using Fuyu.Common.Networking;

namespace Fuyu.Backend.EFT.Networking
{
    public class EftHttpContext : HttpContext
    {
        public EftHttpContext(HttpListenerRequest request, HttpListenerResponse response) : base(request, response)
        {
        }

        public override byte[] GetBinary()
        {
            using (var ms = new MemoryStream())
            {
                Request.InputStream.CopyTo(ms);

                var body = ms.ToArray();
                var encryption = GetEncryption();

                if (!string.IsNullOrWhiteSpace(encryption))
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

                if (MemoryZlib.IsCompressed(body))
                {
                    body = MemoryZlib.Decompress(body);
                }

                return body;
            }
        }

        protected Task SendAsync(byte[] data, string mime, HttpStatusCode status, bool zipped, bool encrypted)
        {
            var hasData = !(data == null);

            // Used for postman debugging by Nexus4880
            // -- seionmoya, 2024-11-18
#if DEBUG
            if (Request.Headers["X-Require-Plaintext"] != null)
            {
                zipped = false;
                encrypted = false;
            }
#endif

            if (hasData && zipped)
            {
                data = MemoryZlib.Compress(data, CompressionLevel.SmallestSize);
            }

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

        public Task SendBinaryAsync(byte[] data, string mime, bool zipped, bool encrypted)
        {
            return SendAsync(data, mime, HttpStatusCode.OK, zipped, encrypted);
        }

        public Task SendJsonAsync(string text, bool zipped, bool encrypted)
        {
            var encoded = Encoding.UTF8.GetBytes(text);
            var mime = zipped || encrypted
                ? "application/octet-stream"
                : "application/json; charset=utf-8";

            return SendAsync(encoded, mime, HttpStatusCode.OK, zipped, encrypted);
        }

        public string GetEncryption()
        {
            return Request.Headers["X-Encryption"];
        }

        public string GetETag()
        {
            return Request.Headers["If-None-Match"];
        }

        public string GetSessionId()
        {
            return Request.Cookies["PHPSESSID"].Value;
        }

        /// <summary>
        /// Getting the current Eft version from the Request Headers.
        /// </summary>
        /// <returns>Client EFT Version</returns>
        public string GetEftVersion()
        {
            // In header tarkov always sends the current version. (Example: "EFT Client 0.15.5.1.33420")

            // TODO: Replace the "EFT Client" or the same in Arena
            //  -- slejmur, 2025-01-09
            return Request.Headers["App-Version"];
        }
    }
}