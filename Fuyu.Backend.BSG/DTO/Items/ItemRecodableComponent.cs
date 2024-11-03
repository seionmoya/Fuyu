using System.Runtime.Serialization;
using Fuyu.Backend.BSG;
using Newtonsoft.Json.Linq;

namespace Fuyu.Backend.EFT.DTO.Items
{
    [DataContract]
    public class ItemRecodableComponent : IItemComponent
    {
        [DataMember]
        public bool IsEncoded;

		public static object CreateComponent(JObject templateProperties)
		{
			if (!templateProperties.ContainsKey("IsEncoded"))
			{
				return null;
			}

			return new ItemRecodableComponent
			{
				IsEncoded = templateProperties.Value<bool>("IsEncoded")
			};
		}
	}
}