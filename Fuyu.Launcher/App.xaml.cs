using System.Windows;
using Fuyu.Common.IO;

namespace Fuyu.Launcher;

public partial class App : Application
{
    void App_Startup(object sender, StartupEventArgs e)
    {
        // Create main application window
        Terminal.WriteLine("Loading main window...");
        var mainWindow = new MainWindow();
        mainWindow.Show();
    }
}