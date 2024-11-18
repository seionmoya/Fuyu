using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Fuyu.Backend.EFT.Networking;
using Fuyu.Common.IO;

namespace Fuyu.Backend.EFT.Controllers.Http
{
    public partial class FilesController : EftHttpController
    {
        [GeneratedRegex(@"^/files/(?<path>.+)$")]
        private static partial Regex PathExpression();

        public FilesController() : base(PathExpression())
        {
        }

        public override Task RunAsync(EftHttpContext context)
        {
            var parameters = context.GetPathParameters(this);
            var path = parameters["path"];
            var extension = path.Split('.').Last();
            var targetFile = path.Replace('/', '.');
            var resourceLocation = $"database.files.{targetFile}";

            try
            {
                var buffer = Resx.GetBytes("eft", resourceLocation);

                // NOTE: file handling is done in UnityWebRequestTexture.GetTexture
                //       instead of EFT's own HTTP client
                // -- seionmoya, 2024-11-18
                return context.SendBinaryAsync(buffer, $"image/{extension}", false, false);
            }
            catch (Exception ex)
            {
                throw new Exception($"Unhandled file request, resource might not exist: {resourceLocation}", ex);
            }
        }
    }
}