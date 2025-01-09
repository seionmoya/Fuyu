using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.Models.Responses;

[DataContract]
public class ResponseBody<T>
{
    [DataMember]
    public int err { get; set; }

    // emits when 'null'
    [DataMember(EmitDefaultValue = false)]
    public string errmsg { get; set; }

    // emits when 'default(T)'
    [DataMember(EmitDefaultValue = false)]
    public T data { get; set; }

    // emits when '0'
    [DataMember(EmitDefaultValue = false)]
    public uint crc { get; set; }
}