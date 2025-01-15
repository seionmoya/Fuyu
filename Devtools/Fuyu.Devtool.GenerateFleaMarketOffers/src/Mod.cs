using System;
using System.Collections.Generic;
using Fuyu.Common.IO;
using Fuyu.DependencyInjection;
using Fuyu.Modding;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Fuyu.Backend.BSG.ItemTemplates;
using Fuyu.Backend.BSG.Models.Profiles.Info;
using Fuyu.Backend.BSG.Models.Trading;
using Fuyu.Backend.BSG.Services;
using Fuyu.Backend.EFTMain;
using Fuyu.Backend.EFTMain.Services;
using Fuyu.Common.Hashing;

public class GenerateFleaMarketOffersMod : AbstractMod
{
    public override string Id { get; } = "com.project-fika.generatefleamarketoffers";

    public override string Name { get; } = "GenerateFleaMarketOffers";

    public override Task OnLoad(DependencyContainer container)
    {
        var player = new RagfairPlayerUser(new MongoId(true), 344, EMemberCategory.Developer, EMemberCategory.Developer,
            "b1gdeveloper", 69.420f, true);
        new Thread(GenerateOffers).Start();

        return Task.CompletedTask;
    }

    static void GenerateOffers()
    {
        var player = new RagfairPlayerUser(new MongoId(true), 344, EMemberCategory.Developer, EMemberCategory.Developer,
            "b1gdeveloper", 69.420f, true);
        Terminal.WriteLine("Generating offers...");

        var sw = Stopwatch.StartNew();
        var templates = EftOrm.Instance.GetItemTemplates()["data"]!.ToObject<Dictionary<MongoId, ItemTemplate>>();
        var handbook = EftOrm.Instance.GetHandbook();
        var success = 0;
        var failed = 0;

        foreach (var (tid, template) in templates)
        {
            if (template.Type != ENodeType.Item)
            {
                continue;
            }
            
            var entry = handbook.Items.Find(i => i.Id == tid);
            int price = entry?.Price ?? 100;

            if (price <= 0)
            {
                price = 100;
            }

            try
            {
                var rootItem = ItemFactoryService.Instance.CreateItem(template.Id, out var items);
                items.Insert(0, rootItem);

                rootItem.Updatable.StackObjectsCount = 100000;
                
                var createdOffer = RagfairService.Instance.CreateAndAddOffer(
                    player,
                    items,
                    false,
                    [
                        new HandoverRequirement()
                        {
                            Count = price,
                            TemplateId = "5449016a4bdc2d6f028b456f"
                        }
                    ],
                    TimeSpan.FromMinutes(30d),
                    true
                );

                if (createdOffer == null)
                {
                    failed++;
                }
                else
                {
                    success++;
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }

        Terminal.WriteLine(
            $"Done generating offers: {sw.ElapsedMilliseconds}ms, {success} succeeded and {failed} failed");
    }
}