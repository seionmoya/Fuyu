using Fuyu.Common.Networking;

namespace Fuyu.Backend.BSG.Networking
{
    public interface IItemEventController : IRouterController<ItemEventContext>
    {
        public string Action { get; }
    }
}