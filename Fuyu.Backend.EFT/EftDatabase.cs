using System;
using System.Collections.Generic;
using Fuyu.Backend.BSG.Models.Accounts;
using Fuyu.Backend.BSG.Models.Customization;
using Fuyu.Backend.BSG.Models.Profiles;
using Fuyu.Backend.BSG.Models.Profiles.Info;
using Fuyu.Backend.BSG.Models.Responses;
using Fuyu.Common.Collections;
using Fuyu.Common.IO;
using Fuyu.Common.Serialization;
using Newtonsoft.Json.Linq;

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

        internal readonly ThreadObject<BuildsListResponse> DefaultBuilds;

        // TODO
        internal readonly ThreadObject<JObject> AchievementList;
        internal readonly ThreadObject<string> AchievementStatistic;
        internal readonly ThreadObject<JObject> Globals;
        internal readonly ThreadObject<JObject> Handbook;
        internal readonly ThreadObject<JObject> HideoutAreas;
        internal readonly ThreadObject<JObject> HideoutCustomizationOfferList;
        internal readonly ThreadObject<JObject> HideoutProductionRecipes;
        internal readonly ThreadObject<JObject> HideoutQteList;
        internal readonly ThreadObject<string> HideoutSettings;
        internal readonly ThreadObject<JObject> Items;
        internal readonly ThreadObject<JObject> LocalWeather;
        internal readonly ThreadObject<string> Locations;
        internal readonly ThreadObject<JObject> Prestige;
        internal readonly ThreadObject<JObject> Quests;
        internal readonly ThreadObject<JObject> Settings;
        internal readonly ThreadObject<JObject> Traders;
        internal readonly ThreadObject<JObject> Weather;

        /// <summary>
        /// The construction of this class is handled in the <see cref="instance"/> (<see cref="Lazy{T}"/>)
        /// </summary>
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
            DefaultBuilds = new ThreadObject<BuildsListResponse>(null);

            // TODO
            AchievementList = new ThreadObject<JObject>(null);
            AchievementStatistic = new ThreadObject<string>(string.Empty);
            Globals = new ThreadObject<JObject>(null);
            Handbook = new ThreadObject<JObject>(null);
            HideoutAreas = new ThreadObject<JObject>(null);
            HideoutCustomizationOfferList = new ThreadObject<JObject>(null);
            HideoutProductionRecipes = new ThreadObject<JObject>(null);
            HideoutQteList = new ThreadObject<JObject>(null);
            HideoutSettings = new ThreadObject<string>(string.Empty);
            Items = new ThreadObject<JObject>(null);
            LocalWeather = new ThreadObject<JObject>(null);
            Locations = new ThreadObject<string>(string.Empty);
            Prestige = new ThreadObject<JObject>(null);
            Quests = new ThreadObject<JObject>(null);
            Settings = new ThreadObject<JObject>(null);
            Traders = new ThreadObject<JObject>(null);
            Weather = new ThreadObject<JObject>(null);
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
                Accounts.Add(account);

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
                Profiles.Add(profile);

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
                Customizations.Add(kvp.Key, kvp.Value);
            }
        }

        private void LoadCustomizationStorage()
        {
            var json = Resx.GetText("eft", "database.client.customization.storage.json");
            var response = Json.Parse<ResponseBody<CustomizationStorageEntry[]>>(json);

            foreach (var entry in response.data)
            {
                CustomizationStorage.Add(entry);
            }
        }

        private void LoadLanguages()
        {
            var json = Resx.GetText("eft", $"database.locales.client.languages.json");
            var response = Json.Parse<ResponseBody<Dictionary<string, string>>>(json);

            foreach (var kvp in response.data)
            {
                Languages.Add(kvp.Key, kvp.Value);
            }
        }

        private void LoadGlobalLocales()
        {
            var languages = Languages.ToDictionary();

            foreach (var languageId in languages.Keys)
            {
                var json = Resx.GetText("eft", $"database.locales.client.locale-{languageId}.json");
                var response = Json.Parse<ResponseBody<Dictionary<string, string>>>(json);

                GlobalLocales.Add(languageId, response.data);
            }
        }

        private void LoadMenuLocales()
        {
            var languages = Languages.ToDictionary();

            foreach (var languageId in languages.Keys)
            {
                var json = Resx.GetText("eft", $"database.locales.client.menu.locale-{languageId}.json");
                var response = Json.Parse<ResponseBody<MenuLocaleResponse>>(json);

                MenuLocales.Add(languageId, response.data);
            }
        }

        private void LoadDefaultBuilds()
        {
            var json = Resx.GetText("eft", "database.client.builds.list.json");
            var response = Json.Parse<ResponseBody<BuildsListResponse>>(json);
            DefaultBuilds.Set(response.data);
        }

        private void LoadWipeProfiles()
        {
            // profile
            var bearJson = Resx.GetText("eft", "database.profiles.player.unheard-bear.json");
            var usecJson = Resx.GetText("eft", "database.profiles.player.unheard-usec.json");
            var savageJson = Resx.GetText("eft", "database.profiles.player.savage.json");

            WipeProfiles.Add("unheard", new Dictionary<EPlayerSide, Profile>()
            {
                { EPlayerSide.Bear, Json.Parse<Profile>(bearJson) },
                { EPlayerSide.Usec, Json.Parse<Profile>(usecJson) },
                { EPlayerSide.Savage, Json.Parse<Profile>(savageJson) }
            });
        }

        // TODO
        private void LoadUnparsed()
        {
            AchievementList.Set(JObject.Parse(Resx.GetText("eft", "database.client.achievement.list.json")));
            AchievementStatistic.Set(Resx.GetText("eft", "database.client.achievement.statistic.json"));
            Globals.Set(JObject.Parse(Resx.GetText("eft", "database.client.globals.json")));
            Handbook.Set(JObject.Parse(Resx.GetText("eft", "database.client.handbook.templates.json")));
            HideoutAreas.Set(JObject.Parse(Resx.GetText("eft", "database.client.hideout.areas.json")));
            HideoutCustomizationOfferList.Set(JObject.Parse(Resx.GetText("eft", "database.client.hideout.customization.offer.list.json")));
            HideoutProductionRecipes.Set(JObject.Parse(Resx.GetText("eft", "database.client.hideout.production.recipes.json")));
            HideoutQteList.Set(JObject.Parse(Resx.GetText("eft", "database.client.hideout.qte.list.json")));
            HideoutSettings.Set(Resx.GetText("eft", "database.client.hideout.settings.json"));
            Items.Set(JObject.Parse(Resx.GetText("eft", "database.client.items.json")));
            LocalWeather.Set(JObject.Parse(Resx.GetText("eft", "database.client.localGame.weather.json")));
            Locations.Set(Resx.GetText("eft", "database.client.locations.json"));
            Prestige.Set(JObject.Parse(Resx.GetText("eft", "database.client.prestige.list.json")));
            Quests.Set(JObject.Parse(Resx.GetText("eft", "database.client.quest.list.json")));
            Settings.Set(JObject.Parse(Resx.GetText("eft", "database.client.settings.json")));
            Traders.Set(JObject.Parse(Resx.GetText("eft", "database.client.trading.api.traderSettings.json")));
            Weather.Set(JObject.Parse(Resx.GetText("eft", "database.client.weather.json")));
        }
    }
}