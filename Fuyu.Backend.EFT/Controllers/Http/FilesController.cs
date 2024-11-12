using System;
using System.Linq;
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
			var extension = path.Split('.').Last();
			var targetFile = path.Replace('/', '.');
			var resourceLocation = $"database.files.{targetFile}";

			try
			{
				var buffer = Resx.GetBytes("eft", resourceLocation);

				return context.SendBinaryAsync(buffer, $"image/{extension}", false);
			}
			catch (Exception ex)
			{
				throw new Exception($"Unhandled file request, resource might not exist: {resourceLocation}", ex);
			}
		}
	}
}