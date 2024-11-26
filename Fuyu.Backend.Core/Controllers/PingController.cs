using System.Net;
using System.Threading.Tasks;
using Fuyu.Backend.Core.Networking;

namespace Fuyu.Backend.Core.Controllers
{
    public class PingController : CoreHttpController
    {
        public PingController() : base("/ping")
        {
        }

        public override Task RunAsync(CoreHttpContext context)
        {
            return context.SendStatus(HttpStatusCode.OK);
        }
    }
}
