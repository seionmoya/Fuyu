using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Fuyu.Backend.BSG.Models.Responses;
using Fuyu.Common.Hashing;
using Fuyu.Common.Networking;
using Fuyu.Common.Serialization;

namespace Fuyu.Backend.BSG.Services
{
    public class ETagService
    {
        // NOTE: why BSG decided to put the crc is quotes is beyond me
        // -- seionmoya, 2024-11-14
        public static uint GetUIntETag(HttpContext context)
        {
            var value = context.GetETag()
                .Replace("\"", string.Empty);

            if (string.IsNullOrWhiteSpace(value))
            {
                return 0u;
            }

            return Convert.ToUInt32(value);
        }

        public static uint GetCrc(object o)
        {
            var json = Json.Stringify(o);
            var bytes = Encoding.UTF8.GetBytes(json);
            return Crc32.Compute(bytes);
        }

        public static bool IsCacheInvalid(uint cached, uint crc)
        {
            return cached == 0u || cached != crc;
        }

        public static Task SendCachedAsync<TResponse>(HttpContext context, ResponseBody<TResponse> response)
        {
            var cached = GetUIntETag(context);
            var crc = GetCrc(response.data);

            Fuyu.Common.IO.Terminal.WriteLine(cached);
            Fuyu.Common.IO.Terminal.WriteLine(crc);

            if (IsCacheInvalid(cached, crc))
            {
                // outdated client cache
                response.crc = crc;
                return context.SendJsonAsync(Json.Stringify(response));
            }
            else
            {
                // up-to-date client cache
                return context.SendStatus(HttpStatusCode.NoContent);
            }   
        }
    }
}