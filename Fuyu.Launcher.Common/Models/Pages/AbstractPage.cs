using System.IO;
using Fuyu.Common.IO;
using Fuyu.Launcher.Common.Services;

namespace Fuyu.Launcher.Common.Models.Pages;

public abstract class AbstractPage
{
    protected virtual string Id { get; }
    protected virtual string Path { get; }

    protected readonly ContentService ContentService;
    protected readonly MessageService MessageService;
    protected readonly NavigationService NavigationService;

    public AbstractPage()
    {
        // resolve dependencies
        ContentService = ContentService.Instance;
        MessageService = MessageService.Instance;
        NavigationService = NavigationService.Instance;

        // register page
        ContentService.SetOrAddLoader(Path, LoadContent);
        MessageService.SetOrAddHandler(Path, HandleMessage);
    }

    protected virtual Stream LoadContent(string path)
    {
        return Resx.GetStream(Id, Path);
    }

    protected virtual void HandleMessage(string message)
    {
        // intentionally left empty
        // -- seionmoya, 2024-01-11
    }
}