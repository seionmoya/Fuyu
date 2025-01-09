using System.Threading.Tasks;
using Fuyu.Backend.BSG.ItemTemplates;
using Fuyu.Backend.BSG.Models.ItemEvents;
using Fuyu.Backend.BSG.Models.Items;
using Fuyu.Backend.BSG.Networking;
using Fuyu.Backend.BSG.Services;
using Fuyu.Backend.EFT.Services;
using Fuyu.Common.IO;

namespace Fuyu.Backend.EFT.Controllers.ItemEvents
{
    public class HealItemEventController : AbstractItemEventController<HealItemEvent>
    {
        private readonly EftOrm _eftOrm;
        private readonly HealthService _healthService;
        private readonly ItemFactoryService _itemFactoryService;

        public HealItemEventController() : base("Heal")
        {
            _eftOrm = EftOrm.Instance;
            _healthService = HealthService.Instance;
            _itemFactoryService = ItemFactoryService.Instance;
        }

        // This method only finds the item, as well as the index. Actually consuming/deleting the item needs to be done.
        public override Task RunAsync(ItemEventContext context, HealItemEvent request)
        {
            var profile = _eftOrm.GetActiveProfile(context.SessionId);
            var item = profile.Pmc.Inventory.FindItem(request.Item);

            if (item == null)
            {
                Terminal.WriteLine($"Failed to find item {request.Item}");
                return Task.CompletedTask;
            }

            var medKit = item.GetOrCreateUpdatable<ItemMedKitComponent>();

            medKit.HpResource -= request.Count;
            if (medKit.HpResource <= 0)
            {
                profile.Pmc.Inventory.RemoveItem(item);
            }

            var bodyPart = _healthService.GetBodyPart(profile.Pmc.Health, request.BodyPart);
            float toHeal = request.Count;

            if (profile.Pmc.Health.HasEffects)
            {
                var itemProperties = _itemFactoryService.ItemTemplates[item.TemplateId].Props.ToObject<MedsItemProperties>();
                bool itemCanRemove = itemProperties.DamageEffects.IsValue1 && itemProperties.DamageEffects.Value1.Count > 0;

                if (itemCanRemove)
                {
                    foreach (var effectPair in itemProperties.DamageEffects.Value1)
                    {
                        if (bodyPart.Effects.ContainsKey(effectPair.Key))
                        {
                            toHeal -= effectPair.Value.Cost = 0;
                            bodyPart.Effects.Remove(effectPair.Key);
                        }
                    }
                }
            }

            bodyPart.Health.Current += toHeal;

            // TODO:
            // Check BackendConfig for 'HealExperience' and add to PMC profile

            return Task.CompletedTask;
        }
    }
}
