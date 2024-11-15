using System.Threading.Tasks;
using Fuyu.Common.Networking;

namespace Fuyu.Backend.EFT.Controllers.Http
{
    public class HideoutProductionRecipesController : HttpController
    {
        public HideoutProductionRecipesController() : base("/client/hideout/production/recipes")
        {
        }

        public override Task RunAsync(HttpContext context)
        {
            return context.SendJsonAsync(EftOrm.GetHideoutProductionRecipes());
        }
    }
}