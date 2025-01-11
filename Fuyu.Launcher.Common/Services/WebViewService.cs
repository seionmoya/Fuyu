using System;
using Microsoft.Web.WebView2.Core;
using Fuyu.Common.IO;

namespace Fuyu.Launcher.Common.Services;

public class WebViewService
{
    public static WebViewService Instance => instance.Value;
    private static readonly Lazy<WebViewService> instance = new(() => new WebViewService());

    private CoreWebView2 _webview;

    private WebViewService()
    {
        _webview = null;
    }

    public void Initialize(CoreWebView2 webview)
    {
        _webview = webview;

        // add event listeners
        _webview.AddWebResourceRequestedFilter("*", CoreWebView2WebResourceContext.All);
        _webview.NavigationStarting += NavigationStarting;
        _webview.WebResourceRequested += WebResourceRequested;
        _webview.WebMessageReceived += WebMessageReceived;
    }

    // handle all events registered on NavigationStarting
    void NavigationStarting(object sender, CoreWebView2NavigationStartingEventArgs args)
    {
        var url = args.Uri;

        #if DEBUG
        Terminal.WriteLine($"Navigating to: {url}");
        #endif

        if (!NavigationService.IsInternalRequest(url))
        {
            // block non-internal requests due to security concerns
            throw new Exception($"Blocked request {url}");
        }

        // update navigation
        NavigationService.PreviousPage = NavigationService.CurrentPage;
        NavigationService.CurrentPage = url;
    }

    // handle all events on registered AddWebResourceRequestedFilter
    void WebResourceRequested(object sender, CoreWebView2WebResourceRequestedEventArgs args)
    {
        var url = args.Request.Uri;

        #if DEBUG
        Terminal.WriteLine($"Requested resource: {url}");
        #endif

        if (NavigationService.IsInternalRequest(url))
        {
            // grab the data
            var path = NavigationService.GetInternalPath(url);
            var headers = NavigationService.GetHeaders(path);
            var content = ContentService.Load(path);

            // send response
            args.Response = _webview.Environment.CreateWebResourceResponse(content, 200, "OK", headers);
            return;
        }
        else
        {
            // block non-internal requests due to security concerns
            throw new Exception($"Blocked request {url}");
        }
    }

    // handle message received from JS window.chrome._webview.postMessage(text)
    void WebMessageReceived(object sender, CoreWebView2WebMessageReceivedEventArgs args)
    {
        var message = args.TryGetWebMessageAsString();
        var url = NavigationService.CurrentPage;

        if (NavigationService.IsInternalRequest(url))
        {
            var path = NavigationService.GetInternalPath(url);
            MessageService.HandleMessage(path, message);
            return;
        }
        else
        {
            // block non-internal messages due to security concerns
            throw new Exception($"Blocked message {url}");
        }
    }
}