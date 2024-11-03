using System.Runtime.Serialization;
using Fuyu.Backend.BSG;
using Newtonsoft.Json.Linq;

namespace Fuyu.Backend.EFT.DTO.Items
{
    [DataContract]
    public class ItemSideEffectComponent : IItemComponent
    {
        [DataMember]
        public float Value;

		public static object CreateComponent(JObject templateProperties)
		{
			if (!templateProperties.ContainsKey("MaxResource"))
			{
				return null;
			}

			return new ItemSideEffectComponent
			{
				Value = templateProperties.Value<float>("MaxResource")
			};
		}
	}
}