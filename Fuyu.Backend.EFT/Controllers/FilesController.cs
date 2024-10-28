using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Fuyu.Common.IO;
using Fuyu.Common.Networking;

namespace Fuyu.Backend.EFT.Controllers
{
	public partial class FilesController : HttpController
	{
		[GeneratedRegex(@"^/files/(?<path>.+)$")]
		private static partial Regex PathExpression();

		public FilesController() : base(PathExpression())
		{
		}

		public override Task RunAsync(HttpContext context)
		{
			var parameters = context.GetPathParameters(this);
			var path = parameters["path"];
			if (path.Contains("trader/avatar"))
			{
				var extension = path.Split('.').Last();
				using Stream stream = Resx.GetStream("eft", "database.traderavatars.rimuru." + extension);
				byte[] buffer = new byte[stream.Length];
				stream.ReadExactly(buffer, 0, buffer.Length);

				return context.SendBinaryAsync(buffer, $"image/{extension}", false);
			}

			throw new Exception($"Unhandled file path: {path}");
		}
	}
}