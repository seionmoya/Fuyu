using System;
using System.Collections.Generic;
using Fuyu.Backend.BSG.DTO.Services;
using Fuyu.Backend.BSG.Models.Accounts;
using Fuyu.Backend.BSG.Models.Customization;
using Fuyu.Backend.BSG.Models.Profiles;
using Fuyu.Backend.BSG.Models.Profiles.Info;
using Fuyu.Backend.BSG.Models.Responses;
using Fuyu.Common.Collections;
using Fuyu.Common.IO;
using Fuyu.Common.Serialization;

namespace Fuyu.Backend.EFT
{
    // NOTE: The properties of this class should _NEVER_ be accessed from the
    //       outside. Use EftOrm instead.
    // -- seionmoya, 2024/09/04

    public class EftDatabase
    {
		public static EftDatabase Instance => instance.Value;
		private static readonly Lazy<EftDatabase> instance = new(() => new EftDatabase());

		internal readonly ThreadList<EftAccount> Accounts;

        internal readonly ThreadList<EftProfile> Profiles;

        //                                        sessid  aid
        internal readonly ThreadDictionary<string, int> Sessions;

        //                                        custid  template 
        internal readonly ThreadDictionary<string, CustomizationTemplate> Customizations;

        internal readonly ThreadList<CustomizationStorageEntry> CustomizationStorage;

        //                                        langid             key     value
        internal readonly ThreadDictionary<string, Dictionary<string, string>> GlobalLocales;

        //                                        langid  name
        internal readonly ThreadDictionary<string, string> Languages;

        //                                        langid  locale
        internal readonly ThreadDictionary<string, MenuLocaleResponse> MenuLocales;

        //                                        edition            side         profile
        internal readonly ThreadDictionary<string, Dictionary<EPlayerSide, Profile>> WipeProfiles;

        // TODO
        internal readonly ThreadObject<string> AchievementList;
        internal readonly ThreadObject<string> AchievementStatistic;
        internal readonly ThreadObject<string> Globals;
        internal readonly ThreadObject<string> Handbook;
        internal readonly ThreadObject<string> HideoutAreas;
        internal readonly ThreadObject<string> HideoutCustomizationOfferList;
        internal readonly ThreadObject<string> HideoutProductionRecipes;
        internal readonly ThreadObject<string> HideoutQteList;
        internal readonly ThreadObject<string> HideoutSettings;
        internal readonly ThreadObject<string> Items;
        internal readonly ThreadObject<string> LocalWeather;
        internal readonly ThreadObject<string> Locations;
        internal readonly ThreadObject<string> Prestige;
        internal readonly ThreadObject<string> Quests;
        internal readonly ThreadObject<string> Settings;
        internal readonly ThreadObject<string> Traders;
        internal readonly ThreadObject<string> Weather;

        private EftDatabase()
        {
            Accounts = new ThreadList<EftAccount>();
            Profiles = new ThreadList<EftProfile>();
            Sessions = new ThreadDictionary<string, int>();

            Customizations = new ThreadDictionary<string, CustomizationTemplate>();
            CustomizationStorage = new ThreadList<CustomizationStorageEntry>();
            GlobalLocales = new ThreadDictionary<string, Dictionary<string, string>>();
            Languages = new ThreadDictionary<string, string>();
            MenuLocales = new ThreadDictionary<string, MenuLocaleResponse>();
            WipeProfiles = new ThreadDictionary<string, Dictionary<EPlayerSide, Profile>>();

            // TODO
            AchievementList = new ThreadObject<string>(string.Empty);
            AchievementStatistic = new ThreadObject<string>(string.Empty);
            Globals = new ThreadObject<string>(string.Empty);
            Handbook = new ThreadObject<string>(string.Empty);
            HideoutAreas = new ThreadObject<string>(string.Empty);
            HideoutCustomizationOfferList = new ThreadObject<string>(string.Empty);
            HideoutProductionRecipes = new ThreadObject<string>(string.Empty);
            HideoutQteList = new ThreadObject<string>(string.Empty);
            HideoutSettings = new ThreadObject<string>(string.Empty);
            Items = new ThreadObject<string>(string.Empty);
            LocalWeather = new ThreadObject<string>(string.Empty);
            Locations = new ThreadObject<string>(string.Empty);
            Prestige = new ThreadObject<string>(string.Empty);
            Quests = new ThreadObject<string>(string.Empty);
            Settings = new ThreadObject<string>(string.Empty);
            Traders = new ThreadObject<string>(string.Empty);
            Weather = new ThreadObject<string>(string.Empty);
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
            LoadWipeProfiles();

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
                EftOrm.Instance.SetOrAddAccount(account);

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
                EftOrm.Instance.SetOrAddProfile(profile);

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
                EftOrm.Instance.SetOrAddCustomization(kvp.Key, kvp.Value);
            }
        }

        private void LoadCustomizationStorage()
        {
            var json = Resx.GetText("eft", "database.client.customization.storage.json");
            var response = Json.Parse<ResponseBody<CustomizationStorageEntry[]>>(json);

            foreach (var entry in response.data)
            {
                EftOrm.Instance.SetOrAddCustomizationStorage(entry);
            }
        }

        private void LoadLanguages()
        {
            var json = Resx.GetText("eft", $"database.locales.client.languages.json");
            var response = Json.Parse<ResponseBody<Dictionary<string, string>>>(json);

            foreach (var kvp in response.data)
            {
                EftOrm.Instance.SetOrAddLanguage(kvp.Key, kvp.Value);
            }
        }

        private void LoadGlobalLocales()
        {
            var languages = EftOrm.Instance.GetLanguages();

            foreach (var languageId in languages.Keys)
            {
                var json = Resx.GetText("eft", $"database.locales.client.locale-{languageId}.json");
                var response = Json.Parse<ResponseBody<Dictionary<string, string>>>(json);

                EftOrm.Instance.SetOrAddGlobalLocale(languageId, response.data);
            }
        }

        private void LoadMenuLocales()
        {
            var languages = EftOrm.Instance.GetLanguages();

            foreach (var languageId in languages.Keys)
            {
                var json = Resx.GetText("eft", $"database.locales.client.menu.locale-{languageId}.json");
                var response = Json.Parse<ResponseBody<MenuLocaleResponse>>(json);

                EftOrm.Instance.SetOrAddMenuLocale(languageId, response.data);
            }
        }

        private void LoadWipeProfiles()
        {
            // profile
            var bearJson = Resx.GetText("eft", "database.profiles.player.unheard-bear.json");
            var usecJson = Resx.GetText("eft", "database.profiles.player.unheard-usec.json");
            var savageJson = Resx.GetText("eft", "database.profiles.player.savage.json");

            EftOrm.Instance.SetOrAddWipeProfile("unheard", new Dictionary<EPlayerSide, Profile>()
            {
                { EPlayerSide.Bear, Json.Parse<Profile>(bearJson) },
                { EPlayerSide.Usec, Json.Parse<Profile>(usecJson) },
                { EPlayerSide.Savage, Json.Parse<Profile>(savageJson) }
            });
        }

        // TODO
        private void LoadUnparsed()
        {
            AchievementList.Set(Resx.GetText("eft", "database.client.achievement.list.json"));
            AchievementStatistic.Set(Resx.GetText("eft", "database.client.achievement.statistic.json"));
            Globals.Set(Resx.GetText("eft", "database.client.globals.json"));
            Handbook.Set(Resx.GetText("eft", "database.client.handbook.templates.json"));
            HideoutAreas.Set(Resx.GetText("eft", "database.client.hideout.areas.json"));
            HideoutCustomizationOfferList.Set(Resx.GetText("eft", "database.client.hideout.customization.offer.list.json"));
            HideoutProductionRecipes.Set(Resx.GetText("eft", "database.client.hideout.production.recipes.json"));
            HideoutQteList.Set(Resx.GetText("eft", "database.client.hideout.qte.list.json"));
            HideoutSettings.Set(Resx.GetText("eft", "database.client.hideout.settings.json"));
            Items.Set(Resx.GetText("eft", "database.client.items.json"));
            LocalWeather.Set(Resx.GetText("eft", "database.client.localGame.weather.json"));
            Locations.Set(Resx.GetText("eft", "database.client.locations.json"));
            Prestige.Set(Resx.GetText("eft", "database.client.prestige.list.json"));
            Quests.Set(Resx.GetText("eft", "database.client.quest.list.json"));
            Settings.Set(Resx.GetText("eft", "database.client.settings.json"));
            Traders.Set(Resx.GetText("eft", "database.client.trading.api.traderSettings.json"));
            Weather.Set(Resx.GetText("eft", "database.client.weather.json"));
        }
    }
}