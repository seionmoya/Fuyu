using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Fuyu.Common.Serialization;

namespace Fuyu.Common.Networking;

public class HttpContext : WebRouterContext
{
    public HttpContext(HttpListenerRequest request, HttpListenerResponse response) : base(request, response)
    {
    }

    public virtual byte[] GetBinary()
    {
        using (var ms = new MemoryStream())
        {
            Request.InputStream.CopyTo(ms);
            return ms.ToArray();
        }
    }

    public virtual string GetText()
    {
        var body = GetBinary();
        return Encoding.UTF8.GetString(body);
    }

    public virtual T GetJson<T>()
    {
        var json = GetText();
        return Json.Parse<T>(json);
    }

    protected virtual Task SendAsync(byte[] data, string mime, HttpStatusCode status)
    {
        var hasData = !(data == null);

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

    public virtual Task SendStatus(HttpStatusCode status)
    {
        return SendAsync(null, "plain/text", status);
    }

    public virtual Task SendBinaryAsync(byte[] data, string mime)
    {
        return SendAsync(data, mime, HttpStatusCode.OK);
    }

    public virtual Task SendJsonAsync(string text)
    {
        var encoded = Encoding.UTF8.GetBytes(text);
        return SendAsync(encoded, "application/json; charset=utf-8", HttpStatusCode.OK);
    }
}