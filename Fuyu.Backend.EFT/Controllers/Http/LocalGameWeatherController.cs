using System.Threading.Tasks;
using Fuyu.Backend.EFT.Networking;

namespace Fuyu.Backend.EFT.Controllers.Http
{
    public class LocalGameWeatherController : EftHttpController
    {
        public LocalGameWeatherController() : base("/client/localGame/weather")
        {
        }

        public override Task RunAsync(EftHttpContext context)
        {
            // TODO: generate this
            // --seionmoya, 2024-11-18
            var text = EftOrm.Instance.GetLocalWeather();
            return context.SendJsonAsync(text, true, true);
        }
    }
}