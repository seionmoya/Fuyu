using Fuyu.Common.IO;
using Fuyu.Backend.Core;
using Fuyu.Backend.Core.Servers;
using Fuyu.Backend.EFT;
using Fuyu.Backend.EFT.Servers;
using Fuyu.Backend.BSG.Services;
using Fuyu.DependencyInjection;
using Fuyu.Modding;
using System.Threading.Tasks;
using Fuyu.Common.Networking;

namespace Fuyu.Backend
{
    public class Program
    {
        static async Task Main()
        {
            var container = new DependencyContainer();

            CoreDatabase.Instance.Load();
            EftDatabase.Instance.Load();
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