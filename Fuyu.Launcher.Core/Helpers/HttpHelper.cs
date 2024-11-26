using System;
using System.Collections.Generic;
using System.Text;
using Fuyu.Common.Collections;
using Fuyu.Common.Serialization;
using Fuyu.Launcher.Core.Models;
using Fuyu.Launcher.Core.Networking;
using Fuyu.Launcher.Core.Services;

namespace Fuyu.Launcher.Core.Helpers
{
    public class HttpHelper
    {
        private static ThreadDictionary<string, CoreHttpClient> _httpClients;

        static HttpHelper()
        {
            _httpClients = new ThreadDictionary<string, CoreHttpClient>();

            _httpClients.Set("fuyu", new CoreHttpClient(SettingsService.FuyuAddress, string.Empty));
            _httpClients.Set("eft", new CoreHttpClient(SettingsService.EftAddress, string.Empty));
            _httpClients.Set("arena", new CoreHttpClient(SettingsService.ArenaAddress, string.Empty));
        }

        public static void ResetSessions()
        {
            _httpClients.Set("fuyu", new CoreHttpClient(SettingsService.FuyuAddress, string.Empty));
            _httpClients.Set("eft", new CoreHttpClient(SettingsService.EftAddress, string.Empty));
            _httpClients.Set("arena", new CoreHttpClient(SettingsService.ArenaAddress, string.Empty));
        }

        public static void CreateSession(string id, string address, string sessionId)
        {
            _httpClients.Set(id, new CoreHttpClient(address, sessionId));
        }

        private static HttpResponse<TResponse> HttpGet<TResponse>(CoreHttpClient httpc, string path)
            where TResponse : class
        {
            var response = httpc.Get(path);
            var responseJson = Encoding.UTF8.GetString(response.Body);
            var responseValue = Json.Parse<TResponse>(responseJson);

            return new HttpResponse<TResponse>
            {
                Response = responseValue,
                Error = null
            };
        }

        private static HttpResponse<TResponse> HttpPost<TRequest, TResponse>(CoreHttpClient httpc, string path, TRequest request)
            where TRequest : class
            where TResponse : class
        {
            var requestJson = Json.Stringify(request);
            var requestBytes = Encoding.UTF8.GetBytes(requestJson);

            var response = httpc.Post(path, requestBytes);
            var responseJson = Encoding.UTF8.GetString(response.Body);
            var responseValue = Json.Parse<TResponse>(responseJson);

            return new HttpResponse<TResponse>
            {
                Response = responseValue,
                Error = null
            };
        }

        private static HttpResponse<TResponse> HttpPut<TRequest, TResponse>(CoreHttpClient httpc, string path, TRequest request)
            where TRequest : class
            where TResponse : class
        {
            var requestJson = Json.Stringify(request);
            var requestBytes = Encoding.UTF8.GetBytes(requestJson);

            httpc.Put(path, requestBytes);

            return new HttpResponse<TResponse>
            {
                Response = null,
                Error = null
            };
        }

        public static HttpResponse<TResponse> HttpReq<TRequest, TResponse>(EHttpMethod method, string serverId, string path, TRequest request = null)
            where TRequest : class
            where TResponse : class
        {
            try
            {
                if (!_httpClients.TryGet(serverId, out var httpc))
                {
                    throw new KeyNotFoundException($"Key \"{serverId}\" not found in {nameof(_httpClients)}");
                }

                return method switch
                {
                    EHttpMethod.GET => HttpGet<TResponse>(httpc, path),
                    EHttpMethod.POST => HttpPost<TRequest, TResponse>(httpc, path, request),
                    EHttpMethod.PUT => HttpPut<TRequest, TResponse>(httpc, path, request),
                    _ => throw new ArgumentOutOfRangeException(nameof(method)),
                };
            }
            catch (Exception ex)
            {
                return new HttpResponse<TResponse>
                {
                    Response = null,
                    Error = ex,
                };
            }
        }
    }
}
