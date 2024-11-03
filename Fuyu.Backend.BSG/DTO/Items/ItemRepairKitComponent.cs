using System.Runtime.Serialization;
using Fuyu.Backend.BSG;
using Newtonsoft.Json.Linq;

namespace Fuyu.Backend.EFT.DTO.Items
{
    [DataContract]
    public class ItemRepairKitComponent : IItemComponent
    {
        [DataMember]
        public float Resource;

		public static object CreateComponent(JObject templateProperties)
		{
			if (!templateProperties.ContainsKey("MaxRepairResource"))
			{
				return null;
			}

			return new ItemRepairKitComponent
			{
				Resource = templateProperties.Value<float>("MaxRepairResource")
			};
		}
	}
}