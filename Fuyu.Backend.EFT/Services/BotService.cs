using System.Collections.Generic;
using Fuyu.Common.Hashing;
using Fuyu.Common.IO;
using Fuyu.Common.Serialization;
using Fuyu.Backend.BSG.Models.Bots;
using Fuyu.Backend.BSG.Models.Profiles;
using Fuyu.Backend.BSG.Services;
using System;

namespace Fuyu.Backend.EFT.Services
{
    // NOTE: regarding how BotService is designed;
    // * The goal is to make a very simple bot generator that can easily be
    //   replaced by mods
    // * Profiles are not stored in EftDatabase because ideally a completely
    //   different generation system not depending on this data
    // -- seionmoya, 2024-10-21
    public class BotService
    {
		public static BotService Instance => instance.Value;
		private static readonly Lazy<BotService> instance = new(() => new BotService());

		private readonly Dictionary<EBotRole, string> _profiles;

        private BotService()
        {
            _profiles = new Dictionary<EBotRole, string>()
            {
                { EBotRole.marksman,                    Resx.GetText("eft", "database.bots.marksman.json")                  },
                { EBotRole.assault,                     Resx.GetText("eft", "database.bots.assault.json")                   },
                { EBotRole.bossTest,                    string.Empty                                                        },    // BSG internal
                { EBotRole.bossBully,                   Resx.GetText("eft", "database.bots.bossbully.json")                 },
                { EBotRole.followerTest,                string.Empty                                                        },    // BSG internal
                { EBotRole.followerBully,               Resx.GetText("eft", "database.bots.followerbully.json")             },
                { EBotRole.bossKilla,                   Resx.GetText("eft", "database.bots.bosskilla.json")                 },
                { EBotRole.bossKojaniy,                 Resx.GetText("eft", "database.bots.bosskojaniy.json")               },
                { EBotRole.followerKojaniy,             Resx.GetText("eft", "database.bots.followerkojaniy.json")           },
                { EBotRole.pmcBot,                      Resx.GetText("eft", "database.bots.pmcbot.json")                    },
                { EBotRole.cursedAssault,               string.Empty                                                        },    // TODO: missing
                { EBotRole.bossGluhar,                  Resx.GetText("eft", "database.bots.bossgluhar.json")                },
                { EBotRole.followerGluharAssault,       Resx.GetText("eft", "database.bots.followergluharassault.json")     },
                { EBotRole.followerGluharSecurity,      Resx.GetText("eft", "database.bots.followergluharsecurity.json")    },
                { EBotRole.followerGluharScout,         Resx.GetText("eft", "database.bots.followergluharscout.json")       },
                { EBotRole.followerGluharSnipe,         string.Empty                                                        },    // TODO: missing?
                { EBotRole.followerSanitar,             Resx.GetText("eft", "database.bots.followersanitar.json")           },
                { EBotRole.test,                        string.Empty                                                        },    // BSG internal
                { EBotRole.assaultGroup,                string.Empty                                                        },    // TODO: missing?
                { EBotRole.sectantWarrior,              Resx.GetText("eft", "database.bots.sectantwarrior.json")            },
                { EBotRole.sectantPriest,               Resx.GetText("eft", "database.bots.sectantpriest.json")             },
                { EBotRole.bossTagilla,                 Resx.GetText("eft", "database.bots.bosstagilla.json")               },
                { EBotRole.followerTagilla,             string.Empty                                                        },    // TODO: missing?
                { EBotRole.exUsec,                      Resx.GetText("eft", "database.bots.exusec.json")                    },
                { EBotRole.gifter,                      Resx.GetText("eft", "database.bots.gifter.json")                    },
                { EBotRole.bossKnight,                  Resx.GetText("eft", "database.bots.bossknight.json")                },
                { EBotRole.followerBigPipe,             Resx.GetText("eft", "database.bots.followerbigpipe.json")           },
                { EBotRole.followerBirdEye,             Resx.GetText("eft", "database.bots.followerbirdeye.json")           },
                { EBotRole.bossZryachiy,                Resx.GetText("eft", "database.bots.bosszryachiy.json")              },
                { EBotRole.followerZryachiy,            Resx.GetText("eft", "database.bots.followerzryachiy.json")          },
                { EBotRole.bossBoar,                    Resx.GetText("eft", "database.bots.bossboar.json")                  },
                { EBotRole.followerBoar,                Resx.GetText("eft", "database.bots.followerboar.json")              },
                { EBotRole.arenaFighter,                string.Empty                                                        },    // TODO: missing
                { EBotRole.arenaFighterEvent,           string.Empty                                                        },    // TODO: missing
                { EBotRole.bossBoarSniper,              Resx.GetText("eft", "database.bots.bossboarsniper.json")            },
                { EBotRole.crazyAssaultEvent,           string.Empty                                                        },    // TODO: missing
                { EBotRole.peacefullZryachiyEvent,      string.Empty                                                        },    // TODO: missing
                { EBotRole.sectactPriestEvent,          string.Empty                                                        },    // TODO: missing
                { EBotRole.ravangeZryachiyEvent,        string.Empty                                                        },    // TODO: missing
                { EBotRole.followerBoarClose1,          Resx.GetText("eft", "database.bots.followerboarclose1.json")        },
                { EBotRole.followerBoarClose2,          Resx.GetText("eft", "database.bots.followerboarclose2.json")        },
                { EBotRole.bossKolontay,                Resx.GetText("eft", "database.bots.bosskolontay.json")              },
                { EBotRole.followerKolontayAssault,     Resx.GetText("eft", "database.bots.followerkolontayassault.json")   },
                { EBotRole.followerKolontaySecurity,    Resx.GetText("eft", "database.bots.followerkolontaysecurity.json")  },
                { EBotRole.shooterBTR,                  Resx.GetText("eft", "database.bots.shooterbtr.json")                },
                { EBotRole.bossPartisan,                Resx.GetText("eft", "database.bots.bosspartisan.json")              },
                { EBotRole.spiritWinter,                string.Empty                                                        },    // TODO: missing
                { EBotRole.spiritSpring,                string.Empty                                                        },    // TODO: missing
                { EBotRole.peacemaker,                  string.Empty                                                        },    // TODO: missing
                { EBotRole.pmcBEAR,                     Resx.GetText("eft", "database.bots.pmcbear.json")                   },    // TODO: missing
                { EBotRole.pmcUSEC,                     Resx.GetText("eft", "database.bots.pmcusec.json")                   },    // TODO: missing
                { EBotRole.skier,                       string.Empty                                                        },    // TODO: missing
                { EBotRole.sectantPredvestnik,          Resx.GetText("eft", "database.bots.sectantpredvestnik.json")        },
                { EBotRole.sectantPrizrak,              Resx.GetText("eft", "database.bots.sectantprizrak.json")            },
                { EBotRole.sectantOni,                  Resx.GetText("eft", "database.bots.sectantoni.json")                },
                { EBotRole.infectedAssault,             Resx.GetText("eft", "database.bots.infectedassault.json")           },
                { EBotRole.infectedPmc,                 Resx.GetText("eft", "database.bots.infectedpmc.json")               },
                { EBotRole.infectedCivil,               Resx.GetText("eft", "database.bots.infectedcivil.json")             },
                { EBotRole.infectedLaborant,            Resx.GetText("eft", "database.bots.infectedlaborant.json")          },
                { EBotRole.infectedTagilla,             string.Empty                                                        },    // TODO: missing
            };
        }

        public Profile[] GetBots(BotCondition[] conditions)
        {
            var profiles = new List<Profile>();

            foreach (var condition in conditions)
            {
                // generate amount for profile type
                for (var i = 0; i < condition.Limit; ++i)
                {
                    var profile = GenerateBot(condition.Role, condition.Difficulty);
                    profiles.Add(profile);
                }
            }

            return profiles.ToArray();
        }

        private Profile GenerateBot(EBotRole role, EBotDifficulty difficulty)
        {
            Terminal.WriteLine(role.ToString());

            var profile = Json.Parse<Profile>(_profiles[role]);

            // regenerate all ids
            profile._id = new MongoId(true);
            InventoryService.Instance.RegenerateIds(profile.Inventory);

            // set difficulty
            profile.Info.Settings.BotDifficulty = difficulty;

            return profile;
        }
    }
}