using System;
using System.Collections.Generic;
using Fuyu.Common.IO;
using Microsoft.Web.WebView2.Core;

namespace Fuyu.Launcher.Common.Services
{
    public class MessageService
    {
        //                                 path           msg
        private static readonly Dictionary<string, Action<string>> _messageCallbacks;
        private static CoreWebView2 _webview;

        static MessageService()
        {
            _messageCallbacks = [];
            _webview = null;
        }

        public static void Initialize(CoreWebView2 webview)
        {
            _webview = webview;
        }

        public static void Add(string path, Action<string> callback)
        {
            _messageCallbacks.Add(path, callback);
        }

        public static void HandleMessage(string path, string message)
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
        public static void SendMessage(string text)
        {
            #if DEBUG
            // show received message
            Terminal.WriteLine($"Backend message: {text}");
            #endif

            _webview.PostWebMessageAsString(text);
        }
    }
}