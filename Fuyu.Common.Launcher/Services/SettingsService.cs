using System;
using System.Collections.Generic;
using Fuyu.Common.Launcher.Models.Settings;

namespace Fuyu.Common.Launcher.Services;

public class SettingsService
{
    public static SettingsService Instance => _instance.Value;
    private static readonly Lazy<SettingsService> _instance = new(() => new SettingsService());

    private readonly List<SettingSection> _settings;

    /// <summary>
    /// The construction of this class is handled in the <see cref="_instance"/> (<see cref="Lazy{T}"/>)
    /// </summary>
    private SettingsService()
    {
        _settings = [];
    }

    public void SetOrAddSection(SettingSection section)
    {
        for (var i = 0; i < _settings.Count; i++)
        {
            if (_settings[i].Id == section.Id)
            {
                _settings[i] = section;
                return;
            }
        }

        _settings.Add(section);
    }

    public string GeneratePage(string htmlTemplate)
    {
        var items = new List<GeneratedSettingItem>();
        var page = htmlTemplate;

        foreach (var section in _settings)
        {
            var sectionItems = GenerateSection(section);
            items.AddRange(sectionItems);
        }

        return page;
    }

    GeneratedSettingItem[] GenerateSection(SettingSection section)
    {
        var items = new List<GeneratedSettingItem>();

        // add list subsection
        var listSectionItem = GenerateSectionList(section);
        items.Add(listSectionItem);

        // add subsection start
        var beginSectionitem = new GeneratedSettingItem()
        {
            Content = $"<div id=\"{section.Id}\">"
        }; 
        items.Add(beginSectionitem);

        // add subsection items 
        foreach (var setting in section.Settings)
        {
            switch (setting.Type)
            {
                case ESettingType.Filepath:
                    var item = GenerateFilepathChunk(section, (FileSetting)setting);
                    items.Add(item);
                    break;
            }
        }

        return [.. items];
    }

    GeneratedSettingItem GenerateSectionList(SettingSection section)
    {
        var item = new GeneratedSettingItem();

        //

        return item;
    }

    GeneratedSettingItem GenerateFilepathChunk(SettingSection section, FileSetting setting)
    {
        var item = new GeneratedSettingItem();
        var id = $"{section.Id}_{setting.Id}";

        item.Section = 

        item.Content = string.Empty
            +  "<div class=\"mb-3\">"
            + $"    <label for=\"formFile\" class=\"form-label\">{setting.Description}</label>"
            + $"    <input class=\"form-control\" type=\"file\" value=\"{setting.Value}\" id=\"{id}\">"
            +  "</div>";

        item.Js = string.Empty
            +  "{"
            +  "    type: \"file\","
            + $"    value: document.getElementById(\"{id}\").value"
            +  "},";

        return item;
    }
}