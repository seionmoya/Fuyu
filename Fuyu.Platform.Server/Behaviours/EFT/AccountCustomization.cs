using Fuyu.Platform.Common.Http;
using Fuyu.Platform.Common.IO;

namespace Fuyu.Platform.Server.Behaviours.EFT
{
    public class AccountCustomization : FuyuBehaviour
    {
        private readonly string _response;

        public AccountCustomization()
        {
            _response = Resx.GetText("eft", "database.eft.client.account.customization.json");
        }

        public override void Run(FuyuContext context)
        {
            SendJson(context, _response);
        }
    }
}