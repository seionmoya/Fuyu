using System.Runtime.Serialization;

namespace Fuyu.Common.Launcher.Models.Settings;

public class FileSetting : Setting
{
    [DataMember(Name = "description")]
    public string Description;

    [DataMember(Name = "value")]
    public string Value;
}