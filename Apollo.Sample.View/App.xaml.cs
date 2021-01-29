using Apollo.MVVM.Security;
using Apollo.Sample.View.Windows;
using GalaSoft.MvvmLight.Threading;
using GalaSoft.MvvmLight.Views;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Apollo.Sample.View
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
            Thread.CurrentPrincipal = new SimplePrincipal();

            INavigationService navigationService = null;
            await Task.Run(() =>
            {
                Thread.Sleep(3000);
                navigationService = ViewModelLocator.Resolve<INavigationService>();
            });
            ShutdownMode = ShutdownMode.OnExplicitShutdown;
            MainWindow.Close();
            MainWindow = new MainWindow();
            navigationService.NavigateTo("LoginPage");
            MainWindow.Show();
            ShutdownMode = ShutdownMode.OnMainWindowClose;
        }
    }
}