using System.Runtime.Serialization;
using Fuyu.Common.Hashing;

namespace Fuyu.Backend.BSG.Models.Trading
{
    [DataContract]
    public class TraderTemplate
    {
        [DataMember(Name = "items_buy")]
        public HandbookEntities BuysItems { get; set; }

        [DataMember(Name = "items_buy_prohibited")]
        public HandbookEntities ProhibitedItems { get; set; }

        [DataMember(Name = "transferableItems")]
        public HandbookEntities TransferrableItems { get; set; }

        [DataMember(Name = "prohibitedTransferableItems")]
        public HandbookEntities ProhibitedTransferrableItems { get; set; }

        [DataMember(Name = "_id")]
        public MongoId Id { get; set; }

        [DataMember(Name = "avatar")]
        public string Avatar { get; set; }

        [DataMember(Name = "balance_rub")]
        public long BalanceRUB { get; set; }

        [DataMember(Name = "balance_dol")]
        public long BalanceUSD { get; set; }

        [DataMember(Name = "balance_eur")]
        public long BalanceEUR { get; set; }

        [DataMember(Name = "discount")]
        public float Discount { get; set; }

        [DataMember(Name = "discount_end")]
        public long DiscountEnd { get; set; }

        [DataMember(Name = "buyer_up")]
        public bool BuyerUp { get; set; }

        [DataMember(Name = "sell_modifier_for_prohibited_items")]
        public int ProhibitedItemsSellModifier { get; set; }

        [DataMember(Name = "currency")]
        public ECurrencyType Currency { get; set; }

        [DataMember(Name = "repair")]
        public RepairInfo Repair { get; set; }

        [DataMember(Name = "insurance")]
        public InsuranceInfo Insurance { get; set; }

        [DataMember(Name = "customization_seller")]
        public bool CustomizationSeller { get; set; }

        [DataMember(Name = "medic")]
        public bool Medic { get; set; }

        [DataMember(Name = "availableInRaid")]
        public bool AvailableInRaid { get; set; }

        [DataMember(Name = "unlockedByDefault")]
        public bool UnlockedByDefault { get; set; }

        [DataMember(Name = "nextResupply")]
        public int NextResupply { get; set; }

        [DataMember(Name = "isCanTransferItems")]
        public bool CanTransferItems { get; set; }

        [DataMember(Name = "loyaltyLevels")]
        public LoyaltyInfo[] LoyaltyLevels { get; set; }
    }
}
