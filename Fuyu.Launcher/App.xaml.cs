using System.Windows;

namespace Fuyu.Launcher;

public partial class App : Application
{
    void App_Startup(object sender, StartupEventArgs e)
    {
        // Create main application window
        var mainWindow = new MainWindow();
        mainWindow.Show();
    }
}