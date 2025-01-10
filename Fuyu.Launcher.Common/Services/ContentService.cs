using System;
using System.Collections.Generic;
using System.IO;
using Fuyu.Common.IO;

namespace Fuyu.Launcher.Common.Services;

public class ContentService
{
    //                                path    ret
    private static readonly List<Func<string, Stream>> _loadCallback;

    static ContentService()
    {
        _loadCallback = [];
    }

    public static void Add(Func<string, Stream> callback)
    {
        _loadCallback.Add(callback);
    }

    public static void Add(string id, string filepath, string resxpath)
    {
        _loadCallback.Add((path) => {
            if (path == filepath)
            {
                return Resx.GetStream(id, resxpath);
            }

            return null;
        });
    }

    // return content as a Stream
    public static Stream Load(string path)
    {
        foreach (var callback in _loadCallback)
        {
            var stream = callback(path);

            if (stream == null)
            {
                // handled in different callback
                continue;
            }

            return stream;
        }

        throw new ArgumentException("No content found on path");
    }
}