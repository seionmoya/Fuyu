using System;
using Microsoft.Web.WebView2.Core;
using Fuyu.Common.IO;

namespace Fuyu.Launcher.Common.Services;

public class NavigationService
{
    public const string INTERNAL_DOMAIN = "http://launcher.fuyu.api";
    public static string CurrentPage;
    private static CoreWebView2 _webview;

    static NavigationService()
    {
        CurrentPage = string.Empty;
        _webview = null;
    }

    public static void Initialize(CoreWebView2 webview)
    {
        _webview = webview;
    }

    public static string GetHeaders(string path)
    {
        // get file extension from Uri
        var ext = VFS.GetFileExtension(path);

        // get header from file extension
        var headers = ext switch
        {
            ".html" => "Content-Type: text/html",
            ".css"  => "Content-Type: text/css",
            ".js"   => "Content-Type: application/javascript",
            ".ico"  => "Content-Type: image/x-icon",
            ".png"  => "Content-Type: image/png",
            _ => throw new NotSupportedException(ext)
        };

        return headers;
    }

    public static bool IsInternalRequest(string url)
    {
        return url.StartsWith(INTERNAL_DOMAIN);
    }

    public static string GetInternalPath(string url)
    {
        return url.Replace(INTERNAL_DOMAIN + "/", string.Empty);
    }

    public static string GetInternalUrl(string path)
    {            
        return $"{INTERNAL_DOMAIN}/{path}";
    }

    public static void Navigate(string url)
    {
        #if DEBUG
        // show received message
        Terminal.WriteLine($"Backend redirect: {url}");
        #endif

        _webview.Navigate(url);
    }
}