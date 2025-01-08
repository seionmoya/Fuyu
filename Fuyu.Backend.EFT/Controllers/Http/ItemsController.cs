using System.Threading.Tasks;
using Fuyu.Backend.EFT.Networking;

namespace Fuyu.Backend.EFT.Controllers.Http
{
    public class ItemsController : AbstractEftHttpController
    {
        private readonly EftOrm _eftOrm;

        public ItemsController() : base("/client/items")
        {
            _eftOrm = EftOrm.Instance;
        }

        public override Task RunAsync(EftHttpContext context)
        {
            // TODO: generate this
            // --seionmoya, 2024-11-18
            var text = _eftOrm.GetItems();
            return context.SendJsonAsync(text, true, true);
        }
    }
}