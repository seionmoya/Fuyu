using System;
using System.Collections.Generic;
using Fuyu.Backend.BSG.Models.Accounts;
using Fuyu.Backend.BSG.Models.Customization;
using Fuyu.Backend.BSG.Models.Profiles;
using Fuyu.Backend.BSG.Models.Profiles.Info;
using Fuyu.Backend.BSG.Models.Responses;

namespace Fuyu.Backend.EFT
{
    public class EftOrm
    {
		public static EftOrm Instance => instance.Value;
		private static readonly Lazy<EftOrm> instance = new(() => new EftOrm());

		/// <summary>
		/// The construction of this class is handled in the <see cref="instance"/> (<see cref="Lazy{T}"/>)
		/// </summary>
		private EftOrm()
		{

		}

		#region Profile
		public List<EftProfile> GetProfiles()
        {
            return EftDatabase.Instance.Profiles.ToList();
        }

        public EftProfile GetProfile(string profileId)
        {
            var profiles = GetProfiles();

            for (var i = 0; i < profiles.Count; ++i)
            {
                if (profiles[i].Pmc._id == profileId)
                {
                    return profiles[i];
                }
            }

            return null;
        }

        public EftProfile GetActiveProfile(string sessionId)
        {
            return GetActiveProfile(GetAccount(sessionId));
        }

        public EftProfile GetActiveProfile(EftAccount account)
        {
            var profiles = GetProfiles();
            string profileId = null;

            switch (account.CurrentSession)
            {
                case ESessionMode.Regular:
                    profileId = account.PvpId;
                    break;

                case ESessionMode.Pve:
                    profileId = account.PveId;
                    break;

                default:
                    throw new Exception($"Unhandled session mode: {account.CurrentSession}");
            }

            foreach (var profile in profiles)
            {
                if (profile.Pmc._id == profileId)
                {
                    return profile;
                }
            }

            return null;
        }

        public void SetOrAddProfile(EftProfile profile)
        {
            var profiles = GetProfiles();

            for (var i = 0; i < profiles.Count; ++i)
            {
                if (profiles[i].Pmc._id == profile.Pmc._id)
                {
                    EftDatabase.Instance.Profiles.TrySet(i, profile);
                    return;
                }
            }

            EftDatabase.Instance.Profiles.Add(profile);
        }

        public void RemoveProfile(EftProfile profile)
        {
            var profiles = GetProfiles();

            for (var i = 0; i < profiles.Count; ++i)
            {
                if (profiles[i].Pmc._id == profile.Pmc._id)
                {
                    EftDatabase.Instance.Profiles.TryRemoveAt(i);
                    return;
                }
            }
        }
        #endregion

        #region Account
        public List<EftAccount> GetAccounts()
        {
            return EftDatabase.Instance.Accounts.ToList();
        }

        public EftAccount GetAccount(int accountId)
        {
            var accounts = GetAccounts();

            for (var i = 0; i < accounts.Count; ++i)
            {
                if (accounts[i].Id == accountId)
                {
                    return accounts[i];
                }
            }

            return null;
        }

        public EftAccount GetAccount(string sessionId)
        {
            var accountId = GetSession(sessionId);
            return GetAccount(accountId);
        }

        public void SetOrAddAccount(EftAccount account)
        {
            var accounts = GetAccounts();

            for (var i = 0; i < accounts.Count; ++i)
            {
                if (accounts[i].Id == account.Id)
                {
                    EftDatabase.Instance.Accounts.TrySet(i, account);
                    return;
                }
            }

            EftDatabase.Instance.Accounts.Add(account);
        }

        public void RemoveAccount(EftAccount account)
        {
            var accounts = GetAccounts();

            for (var i = 0; i < accounts.Count; ++i)
            {
                if (accounts[i].Id == account.Id)
                {
                    EftDatabase.Instance.Accounts.TryRemoveAt(i);
                    return;
                }
            }
        }
        #endregion

        #region Session
        public Dictionary<string, int> GetSessions()
        {
            return EftDatabase.Instance.Sessions.ToDictionary();
        }

        public int GetSession(string sessionId)
        {
            if (!EftDatabase.Instance.Sessions.TryGet(sessionId, out var session))
            {
                throw new Exception($"Failed to get session from sessionId: {sessionId}");
            }

            return session;
        }

        public void SetOrAddSession(string sessionId, int accountId)
        {
            if (EftDatabase.Instance.Sessions.ContainsKey(sessionId))
            {
                EftDatabase.Instance.Sessions.Set(sessionId, accountId);
            }
            else
            {
                EftDatabase.Instance.Sessions.Set(sessionId, accountId);
            }
        }

        public void RemoveSession(string sessionId)
        {
            EftDatabase.Instance.Sessions.Remove(sessionId);
        }
        #endregion

        #region Customization
        public Dictionary<string, CustomizationTemplate> GetCustomizations()
        {
            return EftDatabase.Instance.Customizations.ToDictionary();
        }

        public CustomizationTemplate GetCustomization(string customizationId)
        {
            if (!EftDatabase.Instance.Customizations.TryGet(customizationId, out var customizationTemplate))
            {
                throw new Exception($"Failed to get customization with ID '{customizationId}'");
            }

            return customizationTemplate;
        }

        public void SetOrAddCustomization(string customizationId, CustomizationTemplate template)
        {
            if (EftDatabase.Instance.Customizations.ContainsKey(customizationId))
            {
                EftDatabase.Instance.Customizations.Set(customizationId, template);
            }
            else
            {
                EftDatabase.Instance.Customizations.Set(customizationId, template);
            }
        }

        public void RemoveCustomization(string customizationId)
        {
            EftDatabase.Instance.Customizations.Remove(customizationId);
        }
        #endregion

                #region Customization storage
        public List<CustomizationStorageEntry> GetCustomizationStorage()
        {
            return EftDatabase.Instance.CustomizationStorage.ToList();
        }

        public CustomizationStorageEntry GetCustomizationStorage(string customizationId)
        {
            var customization = GetCustomizationStorage();

            for (var i = 0; i < customization.Count; ++i)
            {
                if (customization[i].id == customizationId)
                {
                    return customization[i];
                }
            }

            return null;
        }

        public void SetOrAddCustomizationStorage(CustomizationStorageEntry entry)
        {
            var customization = GetCustomizationStorage();

            for (var i = 0; i < customization.Count; ++i)
            {
                if (customization[i].id == entry.id)
                {
                    EftDatabase.Instance.CustomizationStorage.TrySet(i, entry);
                    return;
                }
            }

            EftDatabase.Instance.CustomizationStorage.Add(entry);
        }

        public void RemoveCustomizationStorage(CustomizationStorageEntry entry)
        {
            var customization = GetCustomizationStorage();

            for (var i = 0; i < customization.Count; ++i)
            {
                if (customization[i].id == entry.id)
                {
                    EftDatabase.Instance.CustomizationStorage.TryRemoveAt(i);
                    return;
                }
            }
        }
        #endregion

        #region Languages
        public Dictionary<string, string> GetLanguages()
        {
            return EftDatabase.Instance.Languages.ToDictionary();
        }

        public string GetLanguage(string languageId)
        {
            if (!EftDatabase.Instance.Languages.TryGet(languageId, out var language))
            {
                throw new Exception($"Failed to get language from languageId: {languageId}");
            }

            return language;
        }

        public void SetOrAddLanguage(string languageId, string name)
        {
            if (EftDatabase.Instance.Languages.ContainsKey(languageId))
            {
                EftDatabase.Instance.Languages.Set(languageId, name);
            }
            else
            {
                EftDatabase.Instance.Languages.Set(languageId, name);
            }
        }

        public void RemoveLanguage(string languageId)
        {
            EftDatabase.Instance.Languages.Remove(languageId);
        }
        #endregion

        #region GlobalLocales
        public Dictionary<string, Dictionary<string, string>> GetGlobalLocales()
        {
            return EftDatabase.Instance.GlobalLocales.ToDictionary();
        }

        public Dictionary<string, string> GetGlobalLocale(string languageId)
        {
            if (!EftDatabase.Instance.GlobalLocales.TryGet(languageId, out var locale))
            {
                throw new Exception($"Failed to get locale '{languageId}'");
            }

            return locale;
        }

        public void SetOrAddGlobalLocale(string languageId, Dictionary<string, string> globalLocale)
        {
            if (EftDatabase.Instance.GlobalLocales.ContainsKey(languageId))
            {
                EftDatabase.Instance.GlobalLocales.Set(languageId, globalLocale);
            }
            else
            {
                EftDatabase.Instance.GlobalLocales.Set(languageId, globalLocale);
            }
        }

        public void RemoveGlobalLocale(string languageId)
        {
            EftDatabase.Instance.GlobalLocales.Remove(languageId);
        }
        #endregion

        #region MenuLocales
        public Dictionary<string, MenuLocaleResponse> GetMenuLocales()
        {
            return EftDatabase.Instance.MenuLocales.ToDictionary();
        }

        public MenuLocaleResponse GetMenuLocale(string languageId)
        {
            if (!EftDatabase.Instance.MenuLocales.TryGet(languageId, out var menuLocale))
            {
                throw new Exception($"Failed to get menu locale from languageId: {languageId}");
            }

            return menuLocale;
        }

        public void SetOrAddMenuLocale(string languageId, MenuLocaleResponse menuLocale)
        {
            if (EftDatabase.Instance.MenuLocales.ContainsKey(languageId))
            {
                EftDatabase.Instance.MenuLocales.Set(languageId, menuLocale);
            }
            else
            {
                EftDatabase.Instance.MenuLocales.Set(languageId, menuLocale);
            }
        }

        public void RemoveMenuLocale(string languageId)
        {
            EftDatabase.Instance.MenuLocales.Remove(languageId);
        }
        #endregion

        #region Wipe profiles
        public Dictionary<string, Dictionary<EPlayerSide, Profile>> GetWipeProfiles()
        {
            return EftDatabase.Instance.WipeProfiles.ToDictionary();
        }

        public Dictionary<EPlayerSide, Profile> GetWipeProfile(string edition)
        {
            if (!EftDatabase.Instance.WipeProfiles.TryGet(edition, out var profiles))
            {
                throw new Exception($"Failed to get profile(s) for edition '{edition}'");
            }

            return profiles;
        }

        public void SetOrAddWipeProfile(string edition, Dictionary<EPlayerSide, Profile> profiles)
        {
            if (EftDatabase.Instance.WipeProfiles.ContainsKey(edition))
            {
                EftDatabase.Instance.WipeProfiles.Set(edition, profiles);
            }
            else
            {
                EftDatabase.Instance.WipeProfiles.Set(edition, profiles);
            }
        }

        public void RemoveWipeProfile(string edition)
        {
            EftDatabase.Instance.WipeProfiles.Remove(edition);
        }
        #endregion

        #region Unparsed
        public string GetAchievementList()
        {
            return EftDatabase.Instance.AchievementList.Get();
        }

        public string GetAchievementStatistic()
        {
            return EftDatabase.Instance.AchievementStatistic.Get();
        }

        public string GetGlobals()
        {
            return EftDatabase.Instance.Globals.Get();
        }

        public string GetHandbook()
        {
            return EftDatabase.Instance.Handbook.Get();
        }

        public string GetHideoutAreas()
        {
            return EftDatabase.Instance.HideoutAreas.Get();
        }

        public string GetHideoutCustomizationOfferList()
        {
            return EftDatabase.Instance.HideoutCustomizationOfferList.Get();
        }

        public string GetHideoutProductionRecipes()
        {
            return EftDatabase.Instance.HideoutProductionRecipes.Get();
        }

        public string GetHideoutQteList()
        {
            return EftDatabase.Instance.HideoutQteList.Get();
        }

        public string GetHideoutSettings()
        {
            return EftDatabase.Instance.HideoutSettings.Get();
        }

        public string GetItems()
        {
            return EftDatabase.Instance.Items.Get();
        }

        public string GetLocalWeather()
        {
            return EftDatabase.Instance.LocalWeather.Get();
        }

        public string GetLocations()
        {
            return EftDatabase.Instance.Locations.Get();
        }

        public string GetPrestige()
        {
            return EftDatabase.Instance.Prestige.Get();
        }

        public string GetQuests()
        {
            return EftDatabase.Instance.Quests.Get();
        }

        public string GetSettings()
        {
            return EftDatabase.Instance.Settings.Get();
        }

        public string GetTraders()
        {
            return EftDatabase.Instance.Traders.Get();
        }

        public string GetWeather()
        {
            return EftDatabase.Instance.Weather.Get();
        }
        #endregion
    }
}