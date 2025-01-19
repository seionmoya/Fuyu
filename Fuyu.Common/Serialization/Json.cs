using Newtonsoft.Json;

namespace Fuyu.Common.Serialization;

public static class Json
{
    private static readonly JsonSerializerSettings _settings = new JsonSerializerSettings
    {
        ContractResolver = new UnionContractResolver(),
        Formatting = Formatting.None
    };

    public static T Parse<T>(string json)
    {
        return JsonConvert.DeserializeObject<T>(json, _settings);
    }

    public static string Stringify(object o)
    {
        return JsonConvert.SerializeObject(o, _settings);
    }

    public static T Clone<T>(object o)
    {
        var json = Stringify(o);
        return Parse<T>(json);
    }
}