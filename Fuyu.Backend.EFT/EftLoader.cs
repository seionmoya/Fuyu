using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Fuyu.Backend.BSG.Models.Accounts;
using Fuyu.Backend.BSG.Models.Customization;
using Fuyu.Backend.BSG.Models.Locations;
using Fuyu.Backend.BSG.Models.Profiles;
using Fuyu.Backend.BSG.Models.Profiles.Info;
using Fuyu.Backend.BSG.Models.Responses;
using Fuyu.Common.IO;
using Fuyu.Common.Serialization;

namespace Fuyu.Backend.EFT
{
    public class EftLoader
    {
        public static EftLoader Instance => instance.Value;
        private static readonly Lazy<EftLoader> instance = new(() => new EftLoader());

        private readonly EftOrm _eftOrm;

        /// <summary>
        /// The construction of this class is handled in the <see cref="instance"/> (<see cref="Lazy{T}"/>)
        /// </summary>
        private EftLoader()
        {
            _eftOrm = EftOrm.Instance;
        }

        // NOTE: load order is VERY important!
        // -- seionmoya, 2024/09/04
        public void Load()
        {
            // set data source
            Resx.SetSource("eft", typeof(EftDatabase).Assembly);

            // load accounts
            LoadAccounts();
            LoadProfiles();
            LoadSessions();

            // load locales
            LoadLanguages();
            LoadGlobalLocales();
            LoadMenuLocales();

            // load templates
            LoadCustomizations();
            LoadCustomizationStorage();
            LoadDefaultBuilds();
            LoadWipeProfiles();
            LoadWorldMap();
            LoadHideoutSettings();
            LoadAchievementStatistics();

            // TODO
            LoadUnparsed();
        }

        private void LoadAccounts()
        {
            var path = "./Fuyu/Accounts/EFT/";

            if (!VFS.DirectoryExists(path))
            {
                VFS.CreateDirectory(path);
            }

            var files = VFS.GetFiles(path);

            foreach (var filepath in files)
            {
                var json = VFS.ReadTextFile(filepath);
                var account = Json.Parse<EftAccount>(json);
                _eftOrm.SetOrAddAccount(account);

                Terminal.WriteLine($"Loaded EFT account {account.Id}");
            }
        }

        private void LoadProfiles()
        {
            var path = "./Fuyu/Profiles/EFT/";

            if (!VFS.DirectoryExists(path))
            {
                VFS.CreateDirectory(path);
            }

            var files = VFS.GetFiles(path);

            foreach (var filepath in files)
            {
                var json = VFS.ReadTextFile(filepath);
                var profile = Json.Parse<EftProfile>(json);
                _eftOrm.SetOrAddProfile(profile);

                Terminal.WriteLine($"Loaded EFT profile {profile.Pmc._id}");
            }
        }

        private void LoadSessions()
        {
            // intentionally empty
            // sessions are created when users are logged in successfully
            // -- seionmoya, 2024/09/06
        }

        private void LoadCustomizations()
        {
            var json = Resx.GetText("eft", "database.client.customization.json");
            var response = Json.Parse<ResponseBody<Dictionary<string, CustomizationTemplate>>>(json);

            foreach (var kvp in response.data)
            {
                _eftOrm.SetOrAddCustomization(kvp.Key, kvp.Value);
            }
        }

        private void LoadCustomizationStorage()
        {
            var json = Resx.GetText("eft", "database.client.customization.storage.json");
            var response = Json.Parse<ResponseBody<CustomizationStorageEntry[]>>(json);

            foreach (var entry in response.data)
            {
                _eftOrm.SetOrAddCustomizationStorage(entry);
            }
        }

        private void LoadLanguages()
        {
            var json = Resx.GetText("eft", $"database.locales.client.languages.json");
            var response = Json.Parse<ResponseBody<Dictionary<string, string>>>(json);

            foreach (var kvp in response.data)
            {
                _eftOrm.SetOrAddLanguage(kvp.Key, kvp.Value);
            }
        }

        private void LoadGlobalLocales()
        {
            var languages = _eftOrm.GetLanguages();

            foreach (var languageId in languages.Keys)
            {
                var json = Resx.GetText("eft", $"database.locales.client.locale-{languageId}.json");
                var response = Json.Parse<ResponseBody<Dictionary<string, string>>>(json);

                _eftOrm.SetOrAddGlobalLocale(languageId, response.data);
            }
        }

        private void LoadMenuLocales()
        {
            var languages = _eftOrm.GetLanguages();

            foreach (var languageId in languages.Keys)
            {
                var json = Resx.GetText("eft", $"database.locales.client.menu.locale-{languageId}.json");
                var response = Json.Parse<ResponseBody<MenuLocaleResponse>>(json);

                _eftOrm.SetOrAddMenuLocale(languageId, response.data);
            }
        }

        private void LoadDefaultBuilds()
        {
            var json = Resx.GetText("eft", "database.client.builds.list.json");
            var response = Json.Parse<ResponseBody<BuildsListResponse>>(json);
            _eftOrm.SetDefaultBuilds(response.data);
        }

        private void LoadWipeProfiles()
        {
            // profile
            var bearJson = Resx.GetText("eft", "database.profiles.player.unheard-bear.json");
            var usecJson = Resx.GetText("eft", "database.profiles.player.unheard-usec.json");
            var savageJson = Resx.GetText("eft", "database.profiles.player.savage.json");

            _eftOrm.SetOrAddWipeProfile("unheard", new Dictionary<EPlayerSide, Profile>()
            {
                { EPlayerSide.Bear, Json.Parse<Profile>(bearJson) },
                { EPlayerSide.Usec, Json.Parse<Profile>(usecJson) },
                { EPlayerSide.Savage, Json.Parse<Profile>(savageJson) }
            });
        }

        private void LoadAchievementStatistics()
        {
            var json = Resx.GetText("eft", "database.client.achievement.statistic.json");
            var statistics = Json.Parse<AchievementStatisticResponse>(json);
            _eftOrm.SetAchievementStatistics(statistics);
        }

        private void LoadWorldMap()
        {
            var json = Resx.GetText("eft", "database.client.locations.json");
            var worldmap = Json.Parse<WorldMap>(json);
            _eftOrm.SetWorldMap(worldmap);
        }

        private void LoadHideoutSettings()
        {
            var json = Resx.GetText("eft", "database.client.hideout.settings.json");
            var settings = Json.Parse<HideoutSettingsResponse>(json);
            _eftOrm.SetHideoutSettings(settings);
        }

        // TODO
        private void LoadUnparsed()
        {
            _eftOrm.SetAchievementList(JObject.Parse(Resx.GetText("eft", "database.client.achievement.list.json")));
            _eftOrm.SetGlobals(JObject.Parse(Resx.GetText("eft", "database.client.globals.json")));
            _eftOrm.SetHandbook(JObject.Parse(Resx.GetText("eft", "database.client.handbook.templates.json")));
            _eftOrm.SetHideoutAreas(JObject.Parse(Resx.GetText("eft", "database.client.hideout.areas.json")));
            _eftOrm.SetHideoutCustomizationOfferList(JObject.Parse(Resx.GetText("eft", "database.client.hideout.customization.offer.list.json")));
            _eftOrm.SetHideoutProductionRecipes(JObject.Parse(Resx.GetText("eft", "database.client.hideout.production.recipes.json")));
            _eftOrm.SetHideoutQteList(JObject.Parse(Resx.GetText("eft", "database.client.hideout.qte.list.json")));
            _eftOrm.SetItemTemplates(JObject.Parse(Resx.GetText("eft", "database.client.items.json")));
            _eftOrm.SetLocalWeather(JObject.Parse(Resx.GetText("eft", "database.client.localGame.weather.json")));
            _eftOrm.SetPrestige(JObject.Parse(Resx.GetText("eft", "database.client.prestige.list.json")));
            _eftOrm.SetQuests(JObject.Parse(Resx.GetText("eft", "database.client.quest.list.json")));
            _eftOrm.SetSettings(JObject.Parse(Resx.GetText("eft", "database.client.settings.json")));
            _eftOrm.SetTraders(JObject.Parse(Resx.GetText("eft", "database.client.trading.api.traderSettings.json")));
            _eftOrm.SetWeather(JObject.Parse(Resx.GetText("eft", "database.client.weather.json")));
        }
    }
}