using System.Collections.Generic;
using Fuyu.Common.IO;
using System;
using Fuyu.Backend.BSG.Models.Locations;

namespace Fuyu.Backend.EFT.Services
{
    public class LocationService
    {
        public static LocationService Instance => instance.Value;
        private static readonly Lazy<LocationService> instance = new(() => new LocationService());

        private EftOrm _eftOrm;

        private readonly Dictionary<string, string> _locationLoot;

        /// <summary>
        /// The construction of this class is handled in the <see cref="instance"/> (<see cref="Lazy{T}"/>)
        /// </summary>
        private LocationService()
        {
            _eftOrm = EftOrm.Instance;

            _locationLoot = new Dictionary<string, string>()
            {
                { "bigmap",         Resx.GetText("eft", "database.locations.bigmap.json")          },
                { "factory4_day",   Resx.GetText("eft", "database.locations.factory4_day.json")    },
                { "factory4_night", Resx.GetText("eft", "database.locations.factory4_night.json")  },
                { "interchange",    Resx.GetText("eft", "database.locations.interchange.json")     },
                { "laboratory",     Resx.GetText("eft", "database.locations.laboratory.json")      },
                { "lighthouse",     Resx.GetText("eft", "database.locations.lighthouse.json")      },
                { "rezervbase",     Resx.GetText("eft", "database.locations.rezervbase.json")      },
                { "sandbox",        Resx.GetText("eft", "database.locations.sandbox.json")         },
                { "shoreline",      Resx.GetText("eft", "database.locations.shoreline.json")       },
                { "tarkovstreets",  Resx.GetText("eft", "database.locations.tarkovstreets.json")   },
                { "woods",          Resx.GetText("eft", "database.locations.woods.json")           }
            };
        }

        // TODO: generate this
        // --seionmoya, 2025-01-09
        public WorldMap GetWorldMap()
        {
            return _eftOrm.GetWorldMap();
        }

        // TODO: generate this
            // --seionmoya, 2024-11-18
        public string GetLoot(string location)
        {
            return _locationLoot[location];
        }
    }
}