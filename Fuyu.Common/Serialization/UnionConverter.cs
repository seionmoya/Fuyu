using System;
using System.Reflection;
using Fuyu.Common.Collections;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace Fuyu.Common.Serialization;

public class UnionConverter : JsonConverter
{
    private readonly PropertyInfo _propertyInfo;

    public UnionConverter()
    {
    }

    public UnionConverter(PropertyInfo propertyInfo)
    {
        _propertyInfo = propertyInfo;
    }

    // NOTE: I don't think this gets ran at all but I'm leaving it
    // -- nexus4880, 2024-10-14
    public override bool CanConvert(Type objectType)
    {
        return objectType.IsAssignableFrom(typeof(IUnion));
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        var token = JToken.ReadFrom(reader);

        if (token.Type == JTokenType.Null)
        {
            return Activator.CreateInstance(objectType);
        }

        if (_propertyInfo == null)
        {
            var tokenObject = token.ToObject(objectType.GenericTypeArguments[0], serializer);

            return Activator.CreateInstance(objectType, tokenObject);
        }

        var underlyingType = Nullable.GetUnderlyingType(_propertyInfo.PropertyType);
        var isNullable = underlyingType != null;
        var rootType = underlyingType ?? _propertyInfo.PropertyType;
        var tokens = _propertyInfo.GetCustomAttribute<UnionMappingsAttribute>()?.Tokens;

        if (tokens == null)
        {
            throw new Exception($"Missing {nameof(UnionMappingsAttribute)}");
        }

        if (tokens.Length != rootType.GenericTypeArguments.Length)
        {
            throw new Exception($"{nameof(UnionMappingsAttribute.Tokens)} count mismatch");
        }

        for (var i = 0; i < tokens.Length; i++)
        {
            if (tokens[i] == token.Type)
            {
                var tokenObject = token.ToObject(rootType.GenericTypeArguments[i], serializer);

                if (!isNullable)
                {
                    return Activator.CreateInstance(objectType, tokenObject);
                }
                else
                {
                    return Activator.CreateInstance(objectType, Activator.CreateInstance(underlyingType, tokenObject));
                }
            }
        }

        throw new Exception($"Unhandled token {token.Type}");
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        serializer.Serialize(writer, ((IUnion)value).Value);
    }
}

public class UnionContractResolver : DefaultContractResolver
{
    protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
    {
        var property = base.CreateProperty(member, memberSerialization);
        var rootType = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;

        if (property.PropertyType.IsGenericType && typeof(IUnion).IsAssignableFrom(rootType))
        {
            property.Converter = new UnionConverter(member as PropertyInfo);
        }

        return property;
    }
}