using System.Threading.Tasks;
using Fuyu.DependencyInjection;
using Fuyu.Modding;

namespace Fuyu.Client.EFT;

public class EFTMod : AbstractMod
{
    public override string Id { get; } = "Fuyu.Common.Client";
    public override string Name { get; } = "Fuyu.Common.Client";

    public override Task OnLoad(DependencyContainer container)
    {
        return Task.CompletedTask;
    }
}