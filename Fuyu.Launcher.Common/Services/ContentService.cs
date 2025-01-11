using System;
using System.Collections.Generic;
using System.IO;
using Fuyu.Common.IO;

namespace Fuyu.Launcher.Common.Services;

public class ContentService
{
    public static ContentService Instance => instance.Value;
    private static readonly Lazy<ContentService> instance = new(() => new ContentService());

    //                                path    ret
    private readonly List<Func<string, Stream>> _loadCallback;

    /// <summary>
    /// The construction of this class is handled in the <see cref="instance"/> (<see cref="Lazy{T}"/>)
    /// </summary>
    private ContentService()
    {
        _loadCallback = [];
    }

    public void Add(Func<string, Stream> callback)
    {
        _loadCallback.Add(callback);
    }

    public void Add(string id, string filepath, string resxpath)
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
    public Stream Load(string path)
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