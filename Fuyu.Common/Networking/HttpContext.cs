using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Fuyu.Common.Compression;
using Fuyu.Common.Serialization;

namespace Fuyu.Common.Networking
{
    public class HttpContext : WebRouterContext
    {
        public HttpContext(HttpListenerRequest request, HttpListenerResponse response) : base(request, response)
        {
        }

        public bool HasBody()
        {
            return Request.HasEntityBody;
        }

        public byte[] GetBinary()
        {
            using (var ms = new MemoryStream())
            {
                Request.InputStream.CopyTo(ms);

                var body = ms.ToArray();

                if (MemoryZlib.IsCompressed(body))
                {
                    body = MemoryZlib.Decompress(body);
                }

                return body;
            }
        }

        public string GetText()
        {
            var body = GetBinary();
            return Encoding.UTF8.GetString(body);
        }

        public T GetJson<T>()
        {
            var json = GetText();
            return Json.Parse<T>(json);
        }

        public string GetETag()
        {
            return Request.Headers["If-None-Match"];
        }

        public string GetSessionId()
        {
            return Request.Cookies["PHPSESSID"].Value;
        }

        protected Task SendAsync(byte[] data, string mime, HttpStatusCode status, bool zipped = true)
        {
            bool hasData = !(data is null);

            // used for plaintext debugging
            if (Request.Headers["fuyu-debug"] != null)
            {
                zipped = false;
            }

            if (hasData && zipped)
            {
                // NOTE: CompressionLevel.SmallestSize does not exist in
                //       .NET 5 and below.
                // -- seionmoya, 2024-10-07

#if NET6_0_OR_GREATER
                var level = CompressionLevel.SmallestSize;
#else
                var level = CompressionLevel.Optimal;
#endif

                data = MemoryZlib.Compress(data, level);
            }

            Response.StatusCode = (int)status;
            Response.ContentType = mime;
            Response.ContentLength64 = hasData ? data.Length : 0;

            if (hasData)
            {
                using (var payload = Response.OutputStream)
                {
                    return payload.WriteAsync(data, 0, data.Length);
                }
            }
            else
            {
                Response.Close();
                return Task.CompletedTask;
            }
        }

        public Task SendStatus(HttpStatusCode status)
        {
            return SendAsync(null, "plain/text", status, false);
        }

        public Task SendBinaryAsync(byte[] data, string mime, bool zipped = true)
        {
            return SendAsync(data, mime, HttpStatusCode.OK, zipped);
        }

        public Task SendJsonAsync(string text, bool zipped = true)
        {
            var encoded = Encoding.UTF8.GetBytes(text);
            var mime = zipped
                ? "application/octet-stream"
                : "application/json; charset=utf-8";

            return SendAsync(encoded, mime, HttpStatusCode.OK, zipped);
        }

        public void Close()
        {
            Response.Close();
        }

        public override string ToString()
        {
            return $"{GetType().Name}:{Path}(HasBody:{HasBody()})";
        }
    }
}