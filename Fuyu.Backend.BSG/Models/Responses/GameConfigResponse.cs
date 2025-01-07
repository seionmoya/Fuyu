using System.Runtime.Serialization;
using Fuyu.Backend.BSG.Models.Servers;

namespace Fuyu.Backend.BSG.Models.Responses
{
    [DataContract]
    public class GameConfigResponse
    {
        // SKIPPED: aid
        // Reason: only used on BSG's internal server

        // SKIPPED: lang
        // Reason: only used on BSG's internal server

        // SKIPPED: languages
        // Reason: only used on BSG's internal server

        // SKIPPED: ndaFree
        // Reason: only used on BSG's internal server

        // SKIPPED: taxomony
        // Reason: only used on BSG's internal server

        // SKIPPED: activeProfileId
        // Reason: only used on BSG's internal server

        [DataMember]
        public Backends backend { get; set; }

        // SKIPPED: useProtobuf
        // Reason: only used on BSG's internal server

        [DataMember]
        public double utc_time { get; set; }

        // SKIPPED: totalInGame
        // Reason: only used on BSG's internal server

        [DataMember]
        public bool reportAvailable { get; set; }

        [DataMember]
        public bool twitchEventMember { get; set; }

        // SKIPPED: sessionMode
        // Reason: only used on BSG's internal server

        [DataMember]
        public PurchasedGames purchasedGames { get; set; }

        // NOTE: in relation to trader "Ref" (is game synced with Arena)
        [DataMember]
        public bool isGameSynced { get; set; }
    }

    [DataContract]
    public class PurchasedGames
    {
        [DataMember]
        public bool eft { get; set; }

        [DataMember]
        public bool arena { get; set; }
    }
}