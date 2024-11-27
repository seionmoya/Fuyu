using Fuyu.Common.IO;
using Fuyu.Backend.Core;
using Fuyu.Backend.Core.Servers;
using Fuyu.Backend.EFT;
using Fuyu.Backend.EFT.Servers;
using Fuyu.Backend.BSG.DTO.Services;
using Fuyu.DependencyInjection;
using Fuyu.Modding;
using System.Threading.Tasks;

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
            coreServer.RegisterServices();
            coreServer.Start();

            var eftMainServer = new EftMainServer();
            eftMainServer.RegisterServices();
            eftMainServer.Start();

			await ModManager.Instance.Load(container);
			Terminal.WaitForInput();
			await ModManager.Instance.UnloadAll();
		}
    }
}