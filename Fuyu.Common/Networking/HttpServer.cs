using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Fuyu.Common.IO;

namespace Fuyu.Common.Networking;

public class HttpServer
{
    private readonly HttpListener _listener;
    private readonly Thread _onRequest;
    public readonly HttpRouter HttpRouter;
    public readonly WsRouter WsRouter;
    public readonly string Address;
    public readonly string Name;
    public readonly string SubProtocol;

    public HttpServer(string name, string address, string subprotocol = null)
    {
        HttpRouter = new HttpRouter();
        WsRouter = new WsRouter();
        Address = address;
        Name = name;
        SubProtocol = subprotocol;

        _listener = new HttpListener();
        _listener.Prefixes.Add(address);

        _onRequest = new Thread(OnRequestAsync);
    }

    private async void OnRequestAsync()
    {
        Thread.CurrentThread.IsBackground = true;

        while (_listener.IsListening)
        {
            var listenerContext = await _listener.GetContextAsync();

            // NOTE: no await to fire and forget in order to handle multiple requests at once
            // -- nexus4880, 2024-10-07
            if (listenerContext.Request.IsWebSocketRequest)
            {
                _ = Task.Run(() => OnWsRequestAsync(listenerContext));
            }
            else
            {
                _ = Task.Run(() => OnHttpRequestAsync(listenerContext));
            }
        }
    }

    private async Task OnHttpRequestAsync(HttpListenerContext listenerContext)
    {
        var context = new HttpContext(listenerContext.Request, listenerContext.Response);

        var time = DateTime.UtcNow.ToString();
        Terminal.WriteLine($"[{Name}][HTTP] {context.Path}");

        try
        {
            await HttpRouter.RouteAsync(context);
        }
        catch (RouteNotFoundException ex)
        {
            Terminal.WriteLine(ex.Message);
            await context.SendStatus(HttpStatusCode.NotFound);
        }
        catch (Exception ex)
        {
            Terminal.WriteLine(ex.Message);
            context.Close();
        }
    }

    private async Task OnWsRequestAsync(HttpListenerContext listenerContext)
    {
        var wsContext = await listenerContext.AcceptWebSocketAsync(SubProtocol);
        var ws = wsContext.WebSocket;

        try
        {
            var context = new WsContext(listenerContext.Request, listenerContext.Response, ws);
            var time = DateTime.UtcNow.ToString();
            Terminal.WriteLine($"[{Name}][WS  ] {context.Path}");
            await WsRouter.RouteAsync(context);
        }
        catch (Exception ex)
        {
            Terminal.WriteLine(ex.Message);
            // NOTE: no need to manually close, websocket will be disposed
            // -- seionmoya, 2024/09/09 
        }
        finally
        {
            ws?.Dispose();
        }
    }

    public void Start()
    {
        _listener.Start();
        _onRequest.Start();

        Terminal.WriteLine($"[{Name}] Started on {Address}");
    }
}