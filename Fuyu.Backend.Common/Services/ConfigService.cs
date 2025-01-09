using System;
using Fuyu.Backend.Common.Models.Config;
using Fuyu.Common.IO;

namespace Fuyu.Backend.Common.Services;
public class ConfigService
{
    public static ConfigService Instance => instance.Value;
    private static readonly Lazy<ConfigService> instance = new(() => new ConfigService());

    public Action Load;
    /// <summary>
    /// The construction of this class is handled in the <see cref="instance"/> (<see cref="Lazy{T}"/>)
    /// </summary>
    private ConfigService()
    {
        Load += LoadConfig;
    }

    // QUESTION: Do we have massive config file or many small files?
    public void LoadConfig()
    {
        if (VFS.Exists("./Fuyu/Config.json"))
        {
            // TODO: Generate all related configs.
        }
    }

    public MainConfig GetConifg()
    {
        // Make sure we load every time for "hot-loading" the file.
        LoadConfig();
        return new();
    }
}
