using System.Threading.Tasks;
using Fuyu.Backend.BSG.Services;
using Fuyu.Backend.Core;
using Fuyu.Backend.Core.Servers;
using Fuyu.Backend.EFT;
using Fuyu.Backend.EFT.Servers;
using Fuyu.Common.IO;
using Fuyu.Common.Networking;
using Fuyu.DependencyInjection;
using Fuyu.Modding;

namespace Fuyu.Backend
{
    public class Program
    {
        static async Task Main()
        {
            var container = new DependencyContainer();

            CoreLoader.Instance.Load();
            EftLoader.Instance.Load();
            TraderDatabase.Instance.Load();
            ItemFactoryService.Instance.Load();

            var coreServer = new CoreServer();
            container.RegisterSingleton<HttpServer, CoreServer>(coreServer);

            coreServer.RegisterServices();
            coreServer.Start();

            var eftMainServer = new EftMainServer();
            container.RegisterSingleton<HttpServer, EftMainServer>(eftMainServer);

            eftMainServer.RegisterServices();
            eftMainServer.Start();

            Terminal.WriteLine("Loading mods...");
            ModManager.Instance.AddMods("./Fuyu/Mods/Backend");
            await ModManager.Instance.Load(container);
            Terminal.WriteLine("Finished loading mods");

            Terminal.WaitForInput();
            await ModManager.Instance.UnloadAll();
        }
    }
}