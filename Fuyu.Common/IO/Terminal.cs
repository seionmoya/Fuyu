using System;

namespace Fuyu.Common.IO;

public static class Terminal
{
    private static readonly object _lock = new object();
    private static string _filepath;

    static Terminal()
    {
        _filepath = "./Fuyu/Logs/trace.log";
    }

    public static void SetLogFile(string filepath)
    {
        _filepath = filepath;
    }

    private static void WriteToFile(string text)
    {
        VFS.WriteTextFile(_filepath, text, true);
    }

    public static void WriteLine(string text)
    {
        var line = $"{text}\n";

        lock (_lock)
        {
            Console.Write(line);
            WriteToFile(line);
        }
    }

    public static void WriteLine(object o)
    {
        if (o == null)
        {
            throw new NullReferenceException();
        }

        WriteLine(o.ToString());
    }

    public static void WaitForInput()
    {
        // Console.ReadKey doesn't work in vscode buildin terminal
        Console.In.ReadLine();
    }
}