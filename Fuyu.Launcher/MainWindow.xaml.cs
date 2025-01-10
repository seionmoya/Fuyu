using System.Windows;
using Microsoft.Web.WebView2.Core;
using Fuyu.Common.IO;
using Fuyu.DependencyInjection;
using Fuyu.Launcher.Common.Services;
using Fuyu.Modding;

namespace Fuyu.Launcher;

public partial class MainWindow : Window
{
    private readonly DependencyContainer _container;
    private CoreWebView2 _webview;

    public MainWindow()
    {
        // initialize variables
        _container = new DependencyContainer();
        _webview = null;

        // initialize page
        InitializeComponent();
        InitializeAsync();
    }

    // lazy initialize _webview
    async void InitializeAsync()
    {
        // initialize webview
        await browser.EnsureCoreWebView2Async(null);
        _webview = browser.CoreWebView2;

        // initialize services
        WebViewService.Initialize(_webview);
        NavigationService.Initialize(_webview);
        MessageService.Initialize(_webview);

        // load mods
        Terminal.WriteLine("Loading mods...");
        ModManager.Instance.AddMods("./Fuyu/Mods/Launcher");
        await ModManager.Instance.Load(_container);

        // load initial page
        var url = NavigationService.GetInternalUrl("index.html");
        NavigationService.Navigate(url);
    }
}