using System.Threading.Tasks;
using Fuyu.Backend.EFT.Networking;

namespace Fuyu.Backend.EFT.Controllers.Http
{
    public class ItemsController : AbstractEftHttpController
    {
        public ItemsController() : base("/client/items")
        {
        }

        public override Task RunAsync(EftHttpContext context)
        {
            // TODO: generate this
            // --seionmoya, 2024-11-18
            var text = EftOrm.Instance.GetItems();
            return context.SendJsonAsync(text, true, true);
        }
    }
}