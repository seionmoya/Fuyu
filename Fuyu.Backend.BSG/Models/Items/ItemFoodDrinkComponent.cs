using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.Models.Items
{
    [DataContract]
    public class ItemFoodDrinkComponent
    {
        [DataMember]
        public float HpPercent;
    }
}