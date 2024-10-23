using Fuyu.Backend.BSG.DTO.Responses;
using Fuyu.Common.Networking;
using Newtonsoft.Json.Linq;

namespace Fuyu.Backend.BSG.ItemEvents
{
    public class ItemEventContext : IRouterContext
    {
        public string SessionId { get; }
        public string Action { get; }
        public int RequestIndex { get; }
        public JToken Data { get; }
        public ItemEventResponse Response { get; }

        public ItemEventContext(string sessionId, string action, int requestIndex, JToken data, ItemEventResponse response)
        {
            SessionId = sessionId;
            Action = action;
            RequestIndex = requestIndex;
            Data = data;
            Response = response;
        }

        public T GetData<T>()
        {
            return Data.ToObject<T>();
        }
    }
}
