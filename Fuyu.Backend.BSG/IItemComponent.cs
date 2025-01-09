using Newtonsoft.Json.Linq;

namespace Fuyu.Backend.BSG
{
    public interface IItemComponent
    {
        static abstract object CreateComponent(JObject templateProperties);
    }
}