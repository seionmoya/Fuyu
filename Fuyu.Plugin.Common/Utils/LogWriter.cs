using System.Reflection;
using BepInEx.Logging;

namespace Fuyu.Plugin.Common.Utils
{
    public class LogWriter
    {
        private static ManualLogSource _log;
        private static string _prefix;

        public static void Initialize(ManualLogSource log, Assembly asm)
        {
            _log = log;
            _prefix = $"[{asm.GetName()}] ";
        }

        public static void WriteLine(string text)
        {
            if (_log == null)
            {
                return;
            }

            _log.Log(LogLevel.None, _prefix + text);
        }
    }
}