using System;
using System.Collections.Generic;
using Fuyu.Common.IO;
using Microsoft.Web.WebView2.Core;

namespace Fuyu.Launcher.Common.Services;

public class MessageService
{
    public static MessageService Instance => instance.Value;
    private static readonly Lazy<MessageService> instance = new(() => new MessageService());

    //                                 path           msg
    private readonly Dictionary<string, Action<string>> _messageCallbacks;
    private CoreWebView2 _webview;

    /// <summary>
    /// The construction of this class is handled in the <see cref="instance"/> (<see cref="Lazy{T}"/>)
    /// </summary>
    private MessageService()
    {
        _messageCallbacks = [];
        _webview = null;
    }

    public void Initialize(CoreWebView2 webview)
    {
        _webview = webview;
    }

    public void Add(string path, Action<string> callback)
    {
        _messageCallbacks.Add(path, callback);
    }

    public void HandleMessage(string path, string message)
    {
        #if DEBUG
        // show received message
        Terminal.WriteLine($"[{path}]: {message}");
        #endif

        if (_messageCallbacks.TryGetValue(path, out var callback))
        {
            callback(message);
            return;
        }

        throw new ArgumentException("No message handler found on path");
    }

    // can be intercepted in JS by window.chrome._webview.addEventListener('message', onMessage)
    public void SendMessage(string text)
    {
        #if DEBUG
        // show received message
        Terminal.WriteLine($"Backend message: {text}");
        #endif

        _webview.PostWebMessageAsString(text);
    }
}