using System.Windows;
using WPFSharp.Globalizer.MVVMExample.ViewModel;

namespace WPFSharp.Globalizer.MVVMExample
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : GlobalizedApplication
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            MainWindowViewModel viewmodel = new MainWindowViewModel();
            MainWindow main = new MainWindow();
            main.DataContext = viewmodel;
            main.Show();
        }
    }
}
