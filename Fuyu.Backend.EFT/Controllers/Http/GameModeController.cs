using System.Threading.Tasks;
using Fuyu.Common.Networking;
using Fuyu.Common.Serialization;
using Fuyu.Backend.BSG.DTO.Responses;
using Fuyu.Backend.BSG.DTO.Requests;
using Fuyu.Backend.EFT.DTO.Accounts;

namespace Fuyu.Backend.EFT.Controllers
{
    public class GameModeController : HttpController<ClientGameModeRequest>
    {
        public GameModeController() : base("/client/game/mode")
        {
        }

		public override Task RunAsync(HttpContext context, ClientGameModeRequest body)
		{
			var account = EftOrm.GetAccount(context.GetSessionId());

			if (body.SessionMode == null)
			{
				body.SessionMode = ESessionMode.Pve;
			}

			account.CurrentSession = body.SessionMode;
			
			var response = new ResponseBody<GameModeResponse>()
			{
				data = new GameModeResponse()
				{
					GameMode = body.SessionMode,
					BackendUrl = "http://localhost:8010"
				}
			};

			return context.SendJsonAsync(Json.Stringify(response));
		}
	}
}