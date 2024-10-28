using System;
using System.IO;
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
				using Stream stream = Resx.GetStream("eft", resourceLocation);
				byte[] buffer = new byte[stream.Length];
				stream.ReadExactly(buffer, 0, buffer.Length);

				return context.SendBinaryAsync(buffer, $"image/{extension}", false);
			}
			catch (Exception ex)
			{
				throw new Exception($"Unhandled file request, resource might not exist: {resourceLocation}", ex);
			}
		}
	}
}