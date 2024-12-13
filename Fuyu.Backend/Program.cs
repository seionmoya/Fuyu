using Fuyu.Common.IO;
using Fuyu.Backend.Core;
using Fuyu.Backend.Core.Servers;
using Fuyu.Backend.EFT;
using Fuyu.Backend.EFT.Servers;
using Fuyu.Backend.BSG.DTO.Services;
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

            CoreDatabase.Load();
            EftDatabase.Load();
            TraderDatabase.Load();
            ItemFactoryService.Load();

            var coreServer = new CoreServer();
            container.RegisterSingleton<HttpServer, CoreServer>(coreServer);

            coreServer.RegisterServices();
            coreServer.Start();

            var eftMainServer = new EftMainServer();
            container.RegisterSingleton<HttpServer, EftMainServer>(eftMainServer);

            eftMainServer.RegisterServices();
            eftMainServer.Start();

            Terminal.WriteLine("Loading mods...");
            ModManager.Instance.AddMods("./Fuyu/Mods");
            await ModManager.Instance.Load(container);
            Terminal.WriteLine("Finished loading mods");

            Terminal.WaitForInput();
            await ModManager.Instance.UnloadAll();
        }
    }
}