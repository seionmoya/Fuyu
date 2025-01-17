using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Fuyu.Common.Launcher.Models.Settings;

public class SettingSection
{
    [DataMember(Name = "id")]
    public string Id;

    [DataMember(Name = "name")]
    public string Name;

    [DataMember(Name = "settings")]
    public List<Setting> Settings;
}