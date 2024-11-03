using System.Runtime.Serialization;
using Fuyu.Backend.BSG;
using Newtonsoft.Json.Linq;

namespace Fuyu.Backend.EFT.DTO.Items
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