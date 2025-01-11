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
        // resolve dependencies
        var container = new DependencyContainer();

        var contentService = ContentService.Instance;
        var messageService = MessageService.Instance;
        var modManager = ModManager.Instance;
        var navigationService = NavigationService.Instance;
        var webViewService = WebViewService.Instance;

        // initialize webview
        await browser.EnsureCoreWebView2Async(null);
        var webview = browser.CoreWebView2;

        // initialize services
        webViewService.Initialize(webview);
        navigationService.Initialize(webview);
        messageService.Initialize(webview);

        // set content
        var id = "Fuyu.Launcher";
        Resx.SetSource(id, this.GetType().Assembly);
        contentService.Add(id, "favicon.ico", "icon.ico");
        contentService.Add(id, "index.html",  "index.html");

        // load mods
        Terminal.WriteLine("Loading mods...");

#if DEBUG
        // NOTE: assumes running inside VSCode or VS2022+
        var modPath = "../../../../Mods/Launcher";
#else
        var modPath = "./Fuyu/Mods/Launcher";
#endif

        modManager.AddMods(modPath);
        await modManager.Load(container);

        // load initial page
        var url = navigationService.GetInternalUrl("index.html");
        navigationService.NavigateInternal(url);
    }
}