using System;
using Fuyu.Common.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Fuyu.Common.Collections;

// NOTE: This type exists so that any Union can be
// deserialized using the UnionConverter. This is
// because one may want to add a Union<T1, T2, T3>
// or even more types later, which this SHOULD
// support in theory, I have not tested it nor will I
// -- nexus, 2024-10-22

[JsonConverter(typeof(UnionConverter))]
public interface IUnion
{
    object Value { get; }
}

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
public class UnionMappingsAttribute : Attribute
{
    public JTokenType[] Tokens { get; set; }

    public UnionMappingsAttribute(params JTokenType[] tokens)
    {
        Tokens = tokens;
    }
}