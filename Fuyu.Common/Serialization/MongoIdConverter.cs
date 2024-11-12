using System;
using Newtonsoft.Json;
using Fuyu.Common.Hashing;

namespace Fuyu.Common.Serialization
{
	public class MongoIdConverter : JsonConverter<MongoId>
	{
		public override MongoId ReadJson(JsonReader reader, Type objectType, MongoId existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			if (reader.Value != null)
			{
				return new MongoId((string)reader.Value);
			}

			return default;
		}

		public override void WriteJson(JsonWriter writer, MongoId value, JsonSerializer serializer)
		{
			writer.WriteValue(value.ToString());
		}
	}
}
