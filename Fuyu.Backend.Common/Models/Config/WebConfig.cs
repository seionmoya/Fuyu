using System.Runtime.Serialization;

namespace Fuyu.Backend.Common.Models.Config;

[DataContract]
public class WebConfig
{
    [DataMember]
    public string FuyuServerAddress { get; set; } = "http://127.0.0.1:8000";

    [DataMember]
    public string EftServerAddress { get; set; } = "http://127.0.0.1:8010";

    [DataMember]
    public string ArenaServerAddress { get; set; } = "http://127.0.0.1:8020";

    // Enable us to make the server HTTPS. We need to make a certificate see below
    [DataMember]
    public bool UseSSL { get; set; } = false;

    [DataMember]
    public string CertificatePath { get; set; } = string.Empty;

    [DataMember]
    public string CertificatePassword { get; set; } = string.Empty;
}
