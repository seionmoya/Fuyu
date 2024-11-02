using System.Collections.Generic;
using Fuyu.Backend.EFT.DTO.Trading;
using Fuyu.Common.Collections;
using Fuyu.Common.Hashing;
using Fuyu.Common.IO;
using Fuyu.Common.Serialization;

namespace Fuyu.Backend.EFT
{
    public static class TraderDatabase
    {
        private static readonly ThreadDictionary<MongoId, TraderTemplate> _traders;

        static TraderDatabase()
        {
            _traders = new ThreadDictionary<MongoId, TraderTemplate>();
        }

        public static void Load()
        {
            var tradersJson = Resx.GetText("eft", "database.client.trading.api.traderSettings.json");
            var traderTemplates = Json.Parse<TraderTemplate[]>(tradersJson);
            foreach (var traderTemplate in traderTemplates)
            {
                _traders.Set(traderTemplate.Id, traderTemplate);
            }
        }

        public static Dictionary<MongoId, TraderTemplate> GetTraderTemplates()
        {
            return _traders.ToDictionary();
        }
    }
}
