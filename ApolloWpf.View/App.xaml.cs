using ApolloWpf.View.Windows;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Threading;
using GalaSoft.MvvmLight.Views;
using System.Threading.Tasks;
using System.Windows;

namespace ApolloWpf.View
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected async override void OnStartup(StartupEventArgs e)
        {
            DispatcherHelper.Initialize();
            base.OnStartup(e);
            await Task.Run(() =>
            {
                new ApplicationInstaller().Install();
            });
            ShutdownMode = ShutdownMode.OnExplicitShutdown;
            MainWindow.Close();
            MainWindow = new MainWindow();
            MainWindow.Show();
            ShutdownMode = ShutdownMode.OnMainWindowClose;

            // navigation vers la vue principale
            SimpleIoc.Default.GetInstance<INavigationService>().NavigateTo("main");
        }
    }
}