using System.Threading.Tasks;
using Fuyu.Backend.EFT.Networking;

namespace Fuyu.Backend.EFT.Controllers.Http
{
    public class WeatherController : EftHttpController
    {
        public WeatherController() : base("/client/weather")
        {
        }

        public override Task RunAsync(EftHttpContext context)
        {
            // TODO: generate this
            // --seionmoya, 2024-11-18
            var text = EftOrm.GetWeather();
            return context.SendJsonAsync(text, true, true);
        }
    }
}