using System.Threading.Tasks;
using Fuyu.Backend.EFT.Networking;

namespace Fuyu.Backend.EFT.Controllers.Http
{
    public class HideoutProductionRecipesController : EftHttpController
    {
        private readonly EftOrm _eftOrm;

        public HideoutProductionRecipesController() : base("/client/hideout/production/recipes")
        {
            _eftOrm = EftOrm.Instance;
        }

        public override Task RunAsync(EftHttpContext context)
        {
            // TODO: generate this
            // --seionmoya, 2024-11-18
            var text = _eftOrm.GetHideoutProductionRecipes();
            return context.SendJsonAsync(text, true, true);
        }
    }
}