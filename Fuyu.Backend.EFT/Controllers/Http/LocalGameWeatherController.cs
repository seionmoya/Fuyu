using System.Threading.Tasks;
using Fuyu.Common.Networking;

namespace Fuyu.Backend.EFT.Controllers.Http
{
    public class LocalGameWeatherController : HttpController
    {
        public LocalGameWeatherController() : base("/client/localGame/weather")
        {
        }

        public override Task RunAsync(HttpContext context)
        {
            return context.SendJsonAsync(EftOrm.GetLocalWeather());
        }
    }
}