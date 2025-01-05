using System.IO;
using NLog;
using NLog.Targets;

[Target(nameof(FuyuTarget))]
public sealed class FuyuTarget : TargetWithLayout 
{
    protected override void InitializeTarget()
    {
        File.WriteAllText("Test.txt", "Hello, world!");
    }
}