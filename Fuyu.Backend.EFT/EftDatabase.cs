using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Fuyu.Backend.BSG.Models.Accounts;
using Fuyu.Backend.BSG.Models.Customization;
using Fuyu.Backend.BSG.Models.Locations;
using Fuyu.Backend.BSG.Models.Profiles;
using Fuyu.Backend.BSG.Models.Profiles.Info;
using Fuyu.Backend.BSG.Models.Responses;
using Fuyu.Common.Collections;
using Fuyu.Common.IO;

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

        internal readonly ThreadObject<AchievementStatisticResponse> AchievementStatistic;
        internal readonly ThreadObject<BuildsListResponse> DefaultBuilds;
        internal readonly ThreadObject<HideoutSettingsResponse> HideoutSettings;
        internal readonly ThreadObject<WorldMap> WorldMap;

        // TODO
        internal readonly ThreadObject<JObject> AchievementList;
        internal readonly ThreadObject<JObject> Globals;
        internal readonly ThreadObject<JObject> Handbook;
        internal readonly ThreadObject<JObject> HideoutAreas;
        internal readonly ThreadObject<JObject> HideoutCustomizationOfferList;
        internal readonly ThreadObject<JObject> HideoutProductionRecipes;
        internal readonly ThreadObject<JObject> HideoutQteList;
        internal readonly ThreadObject<JObject> ItemTemplates;
        internal readonly ThreadObject<JObject> LocalWeather;
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
            AchievementStatistic = new ThreadObject<AchievementStatisticResponse>(null);
            DefaultBuilds = new ThreadObject<BuildsListResponse>(null);
            WorldMap = new ThreadObject<WorldMap>(null);
            HideoutSettings = new ThreadObject<HideoutSettingsResponse>(null);

            // TODO
            AchievementList = new ThreadObject<JObject>(null);
            Globals = new ThreadObject<JObject>(null);
            Handbook = new ThreadObject<JObject>(null);
            HideoutAreas = new ThreadObject<JObject>(null);
            HideoutCustomizationOfferList = new ThreadObject<JObject>(null);
            HideoutProductionRecipes = new ThreadObject<JObject>(null);
            HideoutQteList = new ThreadObject<JObject>(null);
            ItemTemplates = new ThreadObject<JObject>(null);
            LocalWeather = new ThreadObject<JObject>(null);
            Prestige = new ThreadObject<JObject>(null);
            Quests = new ThreadObject<JObject>(null);
            Settings = new ThreadObject<JObject>(null);
            Traders = new ThreadObject<JObject>(null);
            Weather = new ThreadObject<JObject>(null);
        }        
    }
}