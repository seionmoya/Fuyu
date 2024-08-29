using System.Diagnostics;
using System.IO;

namespace Fuyu.Platform.Launcher.Services
{
    public class ProcessService
    {
        private static string GetLaunchArguments(string sessionId, string address)
        {
            var token = $"-token={sessionId}";
            var config = "-config={\"BackendUrl\":\"" + address + "\",\"Version\":\"live\"}";
            var arguments = $"{token} {config}";
            return arguments;
        }

        public static Process StartEft(string cwd, string sessionId, string address)
        {
            return new Process()
            {
                StartInfo = new ProcessStartInfo()
                {
                    Arguments = GetLaunchArguments(sessionId, address),
                    FileName = Path.Combine(cwd, "EscapeFromTarkov.exe"),
                    WorkingDirectory = cwd
                }
            };
        }

        public static Process StartServer(string cwd)
        {
            return new Process()
            {
                StartInfo = new ProcessStartInfo()
                {
                    Arguments = string.Empty,
                    FileName = Path.Combine(cwd, "Fuyu.Server.exe"),
                    WorkingDirectory = cwd
                }
            };
        }
    }
}