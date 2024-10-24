using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Fuyu.Common.Networking;

namespace Fuyu.Backend.EFT.Controllers
{
	public partial class PushNotiferGetWebsocketController : WsController
	{
		private int _tick;

		public PushNotiferGetWebsocketController() : base(PathExpression())
		{
		}

		public override Task RunAsync(WsContext context)
		{
			return Task.CompletedTask;
		}

		[GeneratedRegex("^/push/notifier/getwebsocket/(?<channelId>[A-Za-z0-9]+)$")]
		private static partial Regex PathExpression();
	}
}