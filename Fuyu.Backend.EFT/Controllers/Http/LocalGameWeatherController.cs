using System.Threading.Tasks;
using Fuyu.Backend.EFT.Networking;

namespace Fuyu.Backend.EFT.Controllers.Http
{
    public class LocalGameWeatherController : AbstractEftHttpController
    {
        private readonly EftOrm _eftOrm;

        public LocalGameWeatherController() : base("/client/localGame/weather")
        {
            _eftOrm = EftOrm.Instance;
        }

        public override Task RunAsync(EftHttpContext context)
        {
            // TODO: generate this
            // --seionmoya, 2024-11-18
            var response = _eftOrm.GetLocalWeather();
            var text = response.ToString();
            return context.SendJsonAsync(text, true, true);
        }
    }
}