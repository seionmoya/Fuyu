using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.Models.Items
{
    [DataContract]
    public class ItemFoodDrinkComponent : IItemComponent
    {
        [DataMember]
        public float HpPercent { get; set; }

        public static object CreateComponent(JObject templateProperties)
        {
            if (!templateProperties.ContainsKey("MaxResource"))
            {
                return null;
            }

            return new ItemFoodDrinkComponent
            {
                HpPercent = templateProperties.Value<float>("MaxResource")
            };
        }
    }
}