using System;
using Newtonsoft.Json;
using Fuyu.Common.Collections;

namespace Fuyu.Common.Serialization
{
    public class UnionConverter : JsonConverter
    {
        // NOTE: I don't think this gets ran at all but I'm leaving it
        // -- nexus4880, 2024-10-14
        public override bool CanConvert(Type objectType)
        {
            return objectType.IsAssignableFrom(typeof(IUnion));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            foreach (var type in objectType.GenericTypeArguments)
            {
                try
                {
                    // NOTE: This will intentionally cause an exception because there
                    // is no other way of seeing if a type can be deserialized
                    // -- nexus4880, 2024-10-14
                    var result = serializer.Deserialize(reader, type);
                    if (result == null)
                    {
                        // Cannot gather type info from null...
                        continue;
                    }

                    // NOTE: This intentionally uses Activator.CreateInstance in order
                    // to return a Union<T1, T2> from T1 or T2 directly
                    // -- nexus4880, 2024-10-14
                    return Activator.CreateInstance(objectType, result);
                }
                catch (JsonSerializationException)
                {
                }
            }

            // Assume we will be working with T1
            var defaultT1 = Activator.CreateInstance(objectType.GenericTypeArguments[0]);

            // Return new Union<T1, T2>(T1);
            return Activator.CreateInstance(objectType, defaultT1);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, ((IUnion)value).Value);
        }
    }
}
