using Fuyu.Backend.Core.Controllers;
using Fuyu.Common.Networking;

namespace Fuyu.Backend.Core.Servers;

public class CoreServer : HttpServer
{
    public CoreServer() : base("core", "http://localhost:8000/")
    {
    }

    public void RegisterServices()
    {
        HttpRouter.AddController<PingController>();
        HttpRouter.AddController<AccountLoginController>();
        HttpRouter.AddController<AccountLogoutController>();
        HttpRouter.AddController<AccountRegisterController>();
        HttpRouter.AddController<AccountGamesController>();
    }
}