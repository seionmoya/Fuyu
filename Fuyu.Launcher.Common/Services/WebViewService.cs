using Microsoft.Web.WebView2.Core;
using Fuyu.Common.IO;

namespace Fuyu.Launcher.Common.Services;

public class WebViewService
{
    private static CoreWebView2 _webview;

    static WebViewService()
    {
        _webview = null;
    }

    public static void Initialize(CoreWebView2 webview)
    {
        _webview = webview;

        // add event listeners
        _webview.AddWebResourceRequestedFilter("*", CoreWebView2WebResourceContext.All);
        _webview.NavigationStarting += NavigationStarting;
        _webview.WebResourceRequested += WebResourceRequested;
        _webview.WebMessageReceived += WebMessageReceived;
    }

    // handle all events registered on NavigationStarting
    static void NavigationStarting(object sender, CoreWebView2NavigationStartingEventArgs args)
    {
        #if DEBUG
        // show requested resources
        Terminal.WriteLine($"Navigating to: {args.Uri}");
        #endif

        NavigationService.CurrentPage = args.Uri;
    }

    // handle all events on registered AddWebResourceRequestedFilter
    static void WebResourceRequested(object sender, CoreWebView2WebResourceRequestedEventArgs args)
    {
        var url = args.Request.Uri;

        #if DEBUG
        // show requested resources
        Terminal.WriteLine(url);
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

        // handle others here!
    }

    // handle message received from JS window.chrome._webview.postMessage(text)
    static void WebMessageReceived(object sender, CoreWebView2WebMessageReceivedEventArgs args)
    {
        var message = args.TryGetWebMessageAsString();
        var url = NavigationService.CurrentPage;

        if (NavigationService.IsInternalRequest(url))
        {
            var path = NavigationService.GetInternalPath(url);
            MessageService.HandleMessage(path, message);
            return;
        }

        // handle others here!
    }
}