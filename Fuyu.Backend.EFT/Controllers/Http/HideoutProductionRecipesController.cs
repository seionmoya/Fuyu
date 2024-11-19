using System.Threading.Tasks;
using Fuyu.Backend.EFT.Networking;

namespace Fuyu.Backend.EFT.Controllers.Http
{
    public class HideoutProductionRecipesController : EftHttpController
    {
        public HideoutProductionRecipesController() : base("/client/hideout/production/recipes")
        {
        }

        public override Task RunAsync(EftHttpContext context)
        {
            // TODO: generate this
            // --seionmoya, 2024-11-18
            var text = EftOrm.GetHideoutProductionRecipes();
            return context.SendJsonAsync(text, true, true);
        }
    }
}