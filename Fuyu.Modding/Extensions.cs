using Fuyu.DependencyInjection;

namespace Fuyu.Modding
{
    public static class Extensions
    {
        public static T ResolveMod<T>(this DependencyContainer container, string id) where T : AbstractMod
        {
            return container.Resolve<AbstractMod, T>(id);
        }
    }
}
