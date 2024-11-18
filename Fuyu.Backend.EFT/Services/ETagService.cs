using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Fuyu.Backend.BSG.Models.Responses;
using Fuyu.Backend.EFT.Networking;
using Fuyu.Common.Hashing;
using Fuyu.Common.Serialization;

namespace Fuyu.Backend.EFT.Services
{
    public class ETagService
    {
        public static uint GetUIntETag(EftHttpContext context)
        {
            var value = context.GetETag();

            if (string.IsNullOrWhiteSpace(value))
            {
                return 0u;
            }

            // NOTE: why BSG decided to put the crc is quotes is beyond me
            // -- seionmoya, 2024-11-14
            value = value.Replace("\"", string.Empty);

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

        public static Task SendCachedAsync<TResponse>(EftHttpContext context, ResponseBody<TResponse> response)
        {
            var cached = GetUIntETag(context);
            var crc = GetCrc(response.data);

            if (IsCacheInvalid(cached, crc))
            {
                // outdated client cache
                response.crc = crc;

                var text = Json.Stringify(response);
                return context.SendJsonAsync(text, true, true);
            }
            else
            {
                // up-to-date client cache
                return context.SendStatus(HttpStatusCode.NotModified);
            }
        }
    }
}