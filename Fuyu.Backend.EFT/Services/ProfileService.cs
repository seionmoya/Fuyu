using System;
using Fuyu.Backend.BSG.Models.Accounts;
using Fuyu.Backend.BSG.Models.Profiles;
using Fuyu.Backend.BSG.Models.Profiles.Info;
using Fuyu.Common.Hashing;
using Fuyu.Common.IO;
using Fuyu.Common.Serialization;

namespace Fuyu.Backend.EFT.Services
{
    public class ProfileService
    {
		public static ProfileService Instance => instance.Value;
		private static readonly Lazy<ProfileService> instance = new(() => new ProfileService());

		/// <summary>
		/// The construction of this class is handled in the <see cref="instance"/> (<see cref="Lazy{T}"/>)
		/// </summary>
		private ProfileService()
		{

		}

		public string CreateProfile(int accountId)
        {
            var profile = new EftProfile()
            {
                Pmc = new Profile(),
                Savage = new Profile(),
                Customization = [],
                ShouldWipe = true
            };

            // generate new ids
            var pmcId = new MongoId(true).ToString();
            var savageId = new MongoId(pmcId, 1, false).ToString();

            // set profile info
            profile.Pmc._id = pmcId;
            profile.Pmc.aid = accountId;

            profile.Savage._id = savageId;
            profile.Savage.aid = accountId;

            // store profile
            EftOrm.Instance.SetOrAddProfile(profile);
            WriteToDisk(profile);

            return pmcId;
        }

        public string WipeProfile(EftAccount account, string side, string headId, string voiceId)
        {
            var profile = EftOrm.Instance.GetActiveProfile(account);
            var pmcId = profile.Pmc._id;
            var savageId = profile.Savage._id;

            // create profiles
            var edition = EftOrm.Instance.GetWipeProfile(account.Edition);

            profile.Savage = edition[EPlayerSide.Savage].Clone();

            // NOTE: Case-sensitive
            // -- seionmoya, 2024-10-13
            switch (side)
            {
                case "Bear":
                    profile.Pmc = edition[EPlayerSide.Bear];
                    break;

                case "Usec":
                    profile.Pmc = edition[EPlayerSide.Usec];
                    break;

                default:
                    throw new Exception("Unsupported faction");
            }

            // setup savage
            profile.Savage._id = savageId;
            profile.Savage.aid = account.Id;

            // setup pmc
            var voiceTemplate = EftOrm.Instance.GetCustomization(voiceId);

            profile.Pmc._id = pmcId;
            profile.Pmc.savage = savageId;
            profile.Pmc.aid = account.Id;
            profile.Pmc.Info.Nickname = account.Username;
            profile.Pmc.Info.LowerNickname = account.Username.ToLowerInvariant();
            profile.Pmc.Info.Voice = voiceTemplate._name;
            profile.Pmc.Customization.Head = headId;

            // wipe done
            profile.ShouldWipe = false;

            // store profile
            EftOrm.Instance.SetOrAddProfile(profile);
            WriteToDisk(profile);

            return profile.Pmc._id;
        }

        public void WriteToDisk(EftProfile profile)
        {
            VFS.WriteTextFile(
                $"./Fuyu/Profiles/EFT/{profile.Pmc._id}.json",
                Json.Stringify(profile));
        }
    }
}