using System.Diagnostics;
using System.IO;

namespace Fuyu.Launcher.Core.Services
{
    public class ProcessService
    {
        private static string[] GetLaunchArguments(string sessionId, string address)
        {
            var token = $"-token={sessionId}";
            var config = "-config={\"BackendUrl\":\"" + address + "\",\"Version\":\"live\",\"MatchingVersion\":\"live\"}";

            return [
                "-force-gfx-jobs",
                "native",
                token,
                config
            ];
        }

        public static Process StartEft(string cwd, string sessionId, string address)
        {
            var psi = new ProcessStartInfo()
            {
                FileName = Path.Combine(cwd, "EscapeFromTarkov.exe"),
                WorkingDirectory = cwd
            };

            var args = GetLaunchArguments(sessionId, address);

            foreach (var arg in args)
            {
                psi.ArgumentList.Add(arg);
            }

            return new Process()
            {
                StartInfo = psi
            };
        }

        public static Process StartArena(string cwd, string sessionId, string address)
        {
            var psi = new ProcessStartInfo()
            {
                FileName = Path.Combine(cwd, "EscapeFromTarkovArena.exe"),
                WorkingDirectory = cwd
            };

            var args = GetLaunchArguments(sessionId, address);

            foreach (var arg in args)
            {
                psi.ArgumentList.Add(arg);
            }

            return new Process()
            {
                StartInfo = psi
            };
        }
    }
}