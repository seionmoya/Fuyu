using System.Threading.Tasks;
using Fuyu.Common.Networking;

namespace Fuyu.Backend.EFT.Controllers.Http
{
    public class WeatherController : HttpController
    {
        public WeatherController() : base("/client/weather")
        {
        }

        public override async Task RunAsync(HttpContext context)
        {
            await context.SendJsonAsync(EftOrm.GetWeather());
        }
    }
}