using System.Windows;
using Fuyu.Common.IO;
using Fuyu.DependencyInjection;
using Fuyu.Modding;
using Fuyu.Launcher.Common.Services;

namespace Fuyu.Launcher;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        // initialize page
        InitializeComponent();
        InitializeAsync();
    }

    // lazy initialize _webview
    async void InitializeAsync()
    {
        var container = new DependencyContainer();

        // initialize webview
        await browser.EnsureCoreWebView2Async(null);
        var webview = browser.CoreWebView2;

        // initialize services
        WebViewService.Initialize(webview);
        NavigationService.Initialize(webview);
        MessageService.Initialize(webview);
    
        // load mods
        Terminal.WriteLine("Loading mods...");

#if DEBUG
        // NOTE: assumes running inside VSCode or VS2022+
        var modPath = "../../../../Mods/Launcher";
#else
        var modPath = "./Fuyu/Mods/Launcher";
#endif

        ModManager.Instance.AddMods(modPath);
        await ModManager.Instance.Load(container);

        // load initial page
        var url = NavigationService.GetInternalUrl("index.html");
        NavigationService.Navigate(url);
    }
}