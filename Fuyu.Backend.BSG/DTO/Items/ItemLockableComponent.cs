using System.Runtime.Serialization;
using Fuyu.Backend.BSG;
using Newtonsoft.Json.Linq;

namespace Fuyu.Backend.EFT.DTO.Items
{
    [DataContract]
    public class ItemLockableComponent : IItemComponent
    {
        [DataMember]
        public bool Locked;

		public static object CreateComponent(JObject templateProperties)
		{
			if (!templateProperties.ContainsKey("isSecured")
				|| !templateProperties.Value<bool>("isSecured"))
			{
				return null;
			}

			return new ItemLockableComponent { Locked = true };
		}
	}
}